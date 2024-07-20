param (
   [string]$target = "local"
)

$url = "http://buildversionsapi.${target}"
if($target -eq "local") 
{
	$url = $url + ":8080"
}
$curl = "curl.exe"
$name = "infoservice"
$deployment ="${name}deploy"
$hostname = "${name}.ubk3s"
$registry="registry:5000"
$kubeconfig = "$env:userprofile\.kube\config.${target}"
$kubeseal = "C:/Apps/kubeseal/kubeseal"

$buildVersion = &${curl} -s "${url}/api/BuildVersion/ReadByName/${name}/v1" | ConvertFrom-Json
$version = $buildVersion.Version

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