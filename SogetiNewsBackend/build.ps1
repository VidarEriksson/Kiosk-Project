param (
   [string]$target = "local"
)

$url = "http://buildversionsapi.${target}"
if($target -eq "local") 
{
	$url = $url + ":8080"
}
$curl = "curl.exe"
$name = "SogetiService"
$gname = $name.ToLower()
$registry = "registry.${target}:5000"
$configuration = "production"
$url
$json = "{""ProjectName"":""${name}"",""VersionElement"":""Revision""}"
$buildVersion = &${curl} -s -X 'PUT' "${url}/api/BuildVersion/Increment/v1" -H 'accept: application/json' -H 'Content-Type: application/json' -d "$json" | ConvertFrom-Json
$version = $buildVersion.Version
$buildVersion
$branch = git rev-parse --abbrev-ref HEAD
$commit = git log -1 --pretty=format:"%H"
$description = "${branch}: ${commit}"

docker build -f .\${name}\Dockerfile  --force-rm -t ${registry}/${gname}:${version} --build-arg Version="${Version}" --build-arg Configuration="${Configuration}" --build-arg Description="${description}" .
docker tag ${registry}/${gname}:${version} ${registry}/${gname}:${version} 

docker push ${registry}/${gname}:${version} 

