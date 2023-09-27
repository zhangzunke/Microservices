# Platform Service
### Docker Command
1. Access window c driver via windos subsystem linux: cd "/mnt/c/Mike Zhang/MyGitHub/Microservices"
2. Build docker image at Dockerfile folder: docker build -t zhangzunke/platformservice .
3. Docker run a container: docker run -p 8080:80 -d zhangzunke/platformservice
4. DOkcer login: sudo docker login
4. Docker push: sudo docker push zhangzunke/platformservice
### DNS Setting
1. sudo nano /etc/resolv.conf
2. add google DNS
nameserver 8.8.8.8
nameserver 8.8.4.4
### K8S
1.sudo service docker start
2.minikube start --driver=docker --extra-config=kubelet.runtime-request-timeout=10m
3.alias kubectl="minikube kubectl --"
### Create deployment on K8S
4.kubectl apply -f platforms-depl.yaml
5.kubectl apply -f platforms-np-srv.yaml
### Expose port for service
1.minikube service platformnpservice-srv -n default
The network is limited if using the Docker driver on Darwin, Windows, or WSL, and the Node IP is not reachable directly.
### Restart Kubectl Deployments
kubectl rollout restart deployment platforms-depl
### Open MiniKube Dashboard
minikube dashboard --url
### Create Kubectl Proxy
kubectl proxy

### ImagePullBackOff
eval $(minikube docker-env)

Docker:
sudo update-alternatives --set iptables /usr/sbin/iptables-legacy
sudo update-alternatives --set ip6tables /usr/sbin/ip6tables-legacy
sudo usermod -aG docker $USER && newgrp docker

### Fix Install Ingress Issue
export NO_PROXY=localhost,127.0.0.1,10.96.0.0/12,192.168.59.0/24,192.168.49.0/24,192.168.39.0/24

### Ingress port issue
kubectl port-forward ingress-nginx-controller-7799c6795f-kk7gk 8090:80 -n ingress-nginx &
bg: to move a foreground job to the background
fg: move a background job back to the foreground 

### Create secret for sqlserver
kubectl create secret generic mssql --from-literal=SA_PASSWORD="Password!"
### Minikube tunnel
minikube tunnel: make sure sql server can be accessed in external

### Get Pod logs
kubectl logs platforms-depl-f5954ccbf-8bk8g -c platformservice

https://github.com/microsoft/WSL/issues/6655
https://docs.docker.com/engine/install/ubuntu/
https://github.com/microsoft/WSL/discussions/4872
https://minikube.sigs.k8s.io/docs/start/

### GRPC
