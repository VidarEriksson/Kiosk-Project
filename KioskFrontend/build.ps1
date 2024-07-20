param (
   [string]$target = "local"
)

$name = "Kiosk"
$curl = "curl.exe"
$increase = "Revision"

$url = "http://buildversionsapi.${target}"
if($target -eq "local") 
{
	$url = $url + ":8080"
}
$gname = $name.ToLower()
$registry = "registry.${target}:5000"
$configuration = "production"

$json = "{""ProjectName"":""${name}"",""VersionElement"":""${increase}""}"
$buildVersion = &${curl} -s -X 'PUT' "${url}/api/BuildVersion/Increment/v1" -H 'accept: application/json' -H 'Content-Type: application/json' -d "$json" | ConvertFrom-Json
$version = $buildVersion.Version

$branch = git rev-parse --abbrev-ref HEAD
$commit = git log -1 --pretty=format:"%H"
$description = "${branch}: ${commit}"

#Only row changed except name
docker build -f .\${name}\Dockerfile  --force-rm -t ${registry}/${gname}:${version} --build-arg Version="${version}" --build-arg Configuration="${configuration}" --build-arg Description="${description}" .
docker tag ${registry}/${gname}:${version} ${registry}/${gname}:${version} 

docker push ${registry}/${gname}:${version} 
