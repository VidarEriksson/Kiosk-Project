# Raspberry Pi

En Raspberry Pi är en liten enkortsdator som utvecklats i Storbritannien av Raspberry Pi Foundation i samarbete med Broadcom. Denna kompakta enhet är känd för sin mångsidighet och låga kostnad, vilket gör den tillgänglig för en bred publik av hobbyister, utbildare och professionella. 

Raspberry Pi kan köra olika operativsystem, inklusive Raspberry Pi OS, och används ofta i projekt som rör elektronik, robotik, retrospel och mer. Den har en processor, minne, USB-portar och andra nödvändiga komponenter för en dator, men lagringen sker på ett micro-SD-kort istället för en inbyggd hårddisk. 

Sedan den första modellen lanserades 2012 har flera generationer och modeller släppts, var och en med förbättringar i prestanda och funktionalitet. Raspberry Pi har blivit en viktig resurs i utbildningssyfte och för dem som vill utforska datorvetenskap och programmering på grund av dess öppna design, tillgänglighet samt ett överkomligt pris.

## Uppsättning

För att ladda ner och installera Raspberry Pi OS (tidigare känd som Raspbian) 64-bit på ett SD-kort för användning med Raspberry Pi 5, börjar du med att ladda ner "Raspberry Pi Imager" till din dator. Detta verktyg hjälper dig att enkelt skriva operativsystemet till SD-kortet. 

När du har laddat ner och installerat Imager, sätt in SD-kortet i din dators kortläsare och starta programmet. Välj sedan Raspberry Pi OS (64-bit) från listan över operativsystem och följ instruktionerna för att skriva det till SD-kortet.

Efter att operativsystemet är installerat och SD-kortet är insatt i din Raspberry Pi 5, startar du enheten. Följ instruktionerna och svara på frågorna som ställs under färdigställandet av din Raspberry.

För att sedan aktivera SSH och VNC, öppna terminalen och använd `sudo raspi-config` för att komma åt konfigurationsmenyn. Navigera till 'Interfacing Options' och aktivera både SSH och VNC. Detta gör att du kan fjärransluta till din Raspberry Pi via en annan dator både som terminal och som fjärrskrivbord.

För att ansluta din PC till Raspberry Pi via SSH, behöver du IP-adressen till din Raspberry och ett SSH-klientprogram såsom PuTTY. Ange IP-adressen i SSH-klienten och logga in med dina användaruppgifter. 

VNC fungerar ungefär likadant som Remote Desktop gör i Windows. För att ansluta via VNC behöver du en VNC-klient såsom VNC Viewer. Ange samma IP-adress i VNC-klienten för att starta en fjärrskrivbordsanslutning. Kom ihåg att både SSH och VNC bör konfigureras för att starta automatiskt vid uppstart för enkel åtkomst.

Om du inte vet vad din Raspberry har för IP-adress så kan du öppna en terminal och skriva kommandot `ip a` eller `hostname -I`för att visa alla nätverksgränssnitt och deras konfigurationer. IP-adressen visas efter "inet" och kan vara listad under "eth0" för Ethernet-anslutning eller "wlan0" för Wi-Fi.

**Tänk på!** Om du kör din Raspberry utan bildskärm ansluten så har du ingen tillgång till fjärrskrivbord, Raspberry känner av vid uppstart om det finns en bildskärm och bara om den finns så startas det grafiska gränssnittet!

## På din Windows klient

För att kunna interagera både genom terminal och fjärrskrivbord med en Raspberry Pi så behöver man en del olika programvaror på sin PC.

För terminalläge kan man med fördel använda "PuTTY" som är en grafisk TTY terminal som kopplar upp sig mot terminalläget i Raspberry.

För fjärrskrivbord finns det ett väl använt program som heter "RealVNC Viewer". Detta fungerar enbart om du har en bildskärm ansluten till din Raspberry och visar då i Windows det skrivbord som Raspberry visar. Du interagerar med mus och tangentbord precis som i vilket annat fönster som helst

För att kunna överföra filer mellan en Raspberry Pi och en PC så kan man använda ett program som heter "WinSCP", denna installation innehåller dessutom CLI'er för att kunna köra ssh och scp för direkt kontroll över det man behöver ha tillgång till. I princip kan man använda Powershell och ssh kommandot för att ansluta på samma sätt som nämndes om PuTTY här ovan.

Själv använder jag enbart WinSCP då mina Raspberry's inte har bildskärm och jag även använder mycket Powershell script för att automatisera de saker jag gör på Raspberry (terminaler, fjärrkommandon och filöverföringar).

## Powershell på Raspberry Pi

För att installera Powershell 7 på din Raspberry Pi gör du följande i en terminal (modifiera versionen av Powershell vid behov):

```bash
sudo apt update
sudo apt install wget libssl1.1 libunwind8
sudo mkdir -p /opt/microsoft/powershell/7
wget -O /tmp/powershell.tar.gz https://github.com/PowerShell/PowerShell/releases/download/v7.2.6/powershell-7.2.6-linux-arm64.tar.gz 
sudo tar zxf /tmp/powershell.tar.gz -C /opt/microsoft/powershell/7
sudo chmod +x /opt/microsoft/powershell/7/pwsh
sudo ln -s /opt/microsoft/powershell/7/pwsh /usr/bin/pwsh
```

Prova efteråt genom att skriva kommandot `pwsh` i terminalen så ska en Powershell prompt visa sig och du kan i denna köra både script och kommandon. För att avsluta din Powershell prompt skriver du bara `exit`.

## .NET på Raspberry Pi

.NET fungerar ypperligt på Raspberry Pi eftersom .NET är plattformsoberoende. Man kan köra många olika typer av applikationer såsom API'er, bakgrundstjänster och enklare konsollapplikationer. Det man däremot inte kan köra är grafiska applikationer såsom desktop-klienter, varken WinForms, WPF eller ens MAUI fungerar på en Raspberry Pi.

För att installera .NET på en Raspberry Pi kör man följande i en terminal:

```bash
sudo apt update
sudo mkdir -p /opt/microsoft/dotnet
echo 'export DOTNET_INSTALL_DIR=/opt/microsoft/dotnet' >> ~/.bashrc
echo 'export DOTNET_ROOT=/opt/microsoft/dotnet' >> ~/.bashrc
echo 'export PATH=$PATH:/opt/microsoft/dotnet' >> ~/.bashrc
source ~/.bashrc
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel LTS --install-dir /opt/microsoft/dotnet
sudo ln -s /opt/microsoft/dotnet/dotnet /usr/bin/dotnet
```

Prova efteråt genom att skriva kommandot `dotnet --info`, du ska nu se information om vilken .NET som installerats.

Om du kör grafisk miljö på din Raspberry Pi (bildskärm ansluten) så kan du även installera VS Code på den, inget jag har använt och kan informera om dessvärre. Själv använder jag Visual Studio på min Windowsdator och för över det som behövs med hjälp av SCP-kommandot. 

## Skriva applikationer i Visual Studio för Raspberry Pi

Om vi börjar med en enkel konsollapplikation med `Console.Writeline("Hello world!");` så finns det en del saker man behöver tänka på för att kunna köra denna på en Raspberry Pi.

I csproj-filen som hör till din applikation bör du lägga till följande rad i det överordnade PropertyGroup elementet:

```xml
<PlatformTarget>ARM64</PlatformTarget>
```

Detta är för att kompilatorn ska kunna kompilera allt för rätt plattform och Raspberry kör just på en ARM64 processor.

Skapa sedan en publishprofile för att publicera till mapp, jag brukar ange "Target location" till "bin\Release\net8.0\publish\" och "Target runtime" som "Portable".

Gör sedan en publish av applikationen och öppna sedan en Powershell prompt i den mappen i Windows. 

Logga in på Raspberry med `ssh pi@192.168.1.253` (byt ut användare och IP-adress till det som är aktuellt).

Kör sedan följande kommandon i terminalfönstret:

```
sudo mkdir -p /apps/HelloWorld
sudo chown pi /apps/HelloWorld -R
sudo chmod 777 /apps/HelloWorld -R
```
Logga sedan ut från Raspberry terminal med `exit`.
Kör sedan följande kommando (byt ut <TargetLocation>, användare och IP-adress till det som är aktuellt):

```
scp -r <TargetLocation>\* pi@192.168.1.253:/apps/HelloWorld
```
Filerna kopieras nu från lokal publishfolder till den folder i Raspberry som nyss skapades.

Logga sedan på med ssh igen och kör följande:

```bash
ssh pi@192.168.1.253 
cd /apps/HelloWorld
sudo chmod +x ./HelloWorld.dll
dotnet ./HelloWorld.dll
```

Vi ska nu se meddelandet "Hello World!"  i terminalen.

Avsluta med `exit`.

Vi har nu sett alla steg som involverar att skriva/publicera/kopiera/köra en applikation i Raspberry Pi. Oavsett applikationstyp så gör man hela tiden likadant och det är således läge för att kanske skriva ett Powershell script som gör allt detta i en sekvens:

```powershell
$projectFolder = "H:\TV-Skvaller\Barometer\Barometer"
$project = "${projectFolder}\Barometer.csproj"
$source = "${projectFolder}\bin\release\net8.0\publish"

$user = "pi"
$ip = "192.168.1.253"
$appsFolder = "/apps"
$app = "Barometer"

$preDeployCmd = "ssh.exe ${user}@${ip} ""/usr/bin/sudo bash -c 'mkdir -p ${appsFolder}/${app};chown ${user} ${appsFolder} -R;chmod 777 ${appsFolder} -R;'"""
$deployCmd = "scp.exe -r ""${source}\*"" ""${user}@${ip}:${appsFolder}/${app}"""
$postDeployCmd = "ssh.exe ${user}@${ip} ""/usr/bin/sudo bash -c 'chmod +x ${appsFolder}/${app}/${app}.dll'"" "

dotnet publish ${project} -o ${source}

iex ${preDeployCmd}

iex "${deployCmd}"

iex "${postDeployCmd}"
```

Först skapar vi ett antal variabler för kompilering/publicering (byt värden till det som passar). 

Sedan skapar vi ett antal variabler som behövs för att kunna logga in i Raspberry och hitta till rätt foldrar där.

Sedan sätter vi upp en sträng med allt som behövs för att skapa foldrar och sätta rättigheter i Raspberry, denna strängen kör vi senare som ett predeploy kommando. 

Sedan sätter vi upp en sträng med allt som behövs för själva deploy kommandot. 

Slutligen så skapar vi en sträng med allt som behövs efter filerna har kopierats till Raspberry, denna strängen kör vi senare som ett postdeploy kommando. 

Sedan exekverar vi i tur och ordning kommandot `dotnet publish` som bygger och publicerar vår leverabel, sedan körs predeploy kommandot `iex ${preDeployCmd}`, deploy kommandot `iex ${deployCmd}` och postdeploy kommandot `iex ${postDeployCmd}`. Vi använder Powershells Invoke-Expression (iex) för att exekvera de strängar vi har för respektive. Tyvärr krävs inloggning i Raspberry för alla de tre kommandon som körs under predeploy, deploy och postdeploy.

