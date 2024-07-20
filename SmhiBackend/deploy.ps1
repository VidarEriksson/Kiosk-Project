param (
   [string]$target = "local"
)

$name = "smhiservice"
$curl = "curl.exe"
$kubeseal = "C:/Apps/kubeseal/kubeseal"

$url = "http://buildversionsapi.${target}"
if($target -eq "local") 
{
	$url = $url + ":8080"
}

$deployment ="${name}deploy"
$hostname = "${name}.${target}"
$registry="registry:5000"
$kubeconfig = "$env:userprofile\.kube\config.${target}"

$version = "0.0.0.1"
$alive = &${curl} -s "${url}/api/Ping/v1" -H "accept: text/plain"
if($alive -eq """pong""")
{
	$buildVersion = &${curl} -s "${url}/api/BuildVersion/ReadByName/${name}/v1" | ConvertFrom-Json
	$version = $buildVersion.Version
}

if(Test-Path -Path ./${deployment}/secrets/*)
{
	"Creating secrets"
	kubectl create secret generic ${name}-secret --output json --dry-run=client --from-file=./${deployment}/secrets --kubeconfig $kubeconfig|
		&${kubeseal} -n "${name}" --controller-namespace kube-system --format yaml --kubeconfig $kubeconfig > "./${deployment}/templates/secret.yaml"
}

$image = "${registry}/${name}:${version}"
$cmd = "helm upgrade --install ${name} ${deployment} -n ${name} --create-namespace --set image=""${image}"" --set host=""${hostname}""  --kubeconfig ""${kubeconfig}"""
Invoke-Expression $cmd

if(Test-Path -Path ./${deployment}/templates/secret.yaml)
{
	git checkout ./${deployment}/templates/secret.yaml
}