Kør python scriptet på computeren først for at generere ML modellen (hvilket tager lang tid) 
Få terminalen til at kigge i PassCheckService mappen, som gerne skulle ligge i LogNKey/Services/PassCheckerService

1: Kør denne docker build command for at skabe et image
docker build --no-cache -t passwordchecker .

2: Kør denne docker run command for at skabe en container med navnet "PassCheckContainer", samt køres scriptet også
docker run -i -t --name PassCheckContainer passwordchecker python PassChecker.py
(hvis man indtaster "stop" så stoppes scriptet og dermed containeren)

3: Brug denne command for at starte containeren og scriptet op igen 
docker start -i PassCheckContainer