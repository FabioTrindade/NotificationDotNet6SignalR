# NotificationDotNet6SignalR

# Gerar Imagem
docker build -t notification:1.0 -f NotificationDotNet6SignalR/Dockerfile .

# Executar aplicação
docker run --name notification -it --rm -p 8080:80 notification:1.0
