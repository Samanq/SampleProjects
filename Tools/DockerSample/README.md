# Docker Commands

## Get version of docker

Commands:

> - docker version

Example:

```
> docker version
Client:
 Cloud integration: v1.0.28
 Version:           20.10.17
 API version:       1.41
 Go version:        go1.17.11
 Git commit:        100c701
 Built:             Mon Jun  6 23:09:02 2022
 OS/Arch:           windows/amd64
 Context:           default
 Experimental:      true

Server: Docker Desktop 4.11.0 (83626)
 Engine:
  Version:          20.10.17
  API version:      1.41 (minimum version 1.12)
  Go version:       go1.17.11
  Git commit:       a89b842
  Built:            Mon Jun  6 23:01:23 2022
  OS/Arch:          linux/amd64
  Experimental:     false
 containerd:
  Version:          1.6.6
  GitCommit:        10c12954828e7c7c9b6e0ea9b0c02b01407d3ae1
 runc:
  Version:          1.1.2
  GitCommit:        v1.1.2-0-ga916309
 docker-init:
  Version:          0.19.0
  GitCommit:        de40ad0
```

---

## Run an Image

Commands:

> - docker run \<imageName>

Example:

```
> docker run ubunto
```

---

## Getting List of images

Commands:

> - docker image ls
> - docker images

Example:

```
> docker image ls
REPOSITORY   TAG       IMAGE ID       CREATED       SIZE
ubuntu       latest    27941809078c   8 weeks ago   77.8MB

> docker images
REPOSITORY   TAG       IMAGE ID       CREATED       SIZE
ubuntu       latest    27941809078c   8 weeks ago   77.8MB
```

---

## Getting List of containers

Commands:

> - docker ps [-a]
> - docker container ls [-a]

Note:

> - -a shows all contariners including stoped ones.

Example:

```
> docker ps
CONTAINER ID   IMAGE     COMMAND   CREATED       STATUS         PORTS     NAMES
f6ac9aab4464   ubuntu    "bash"    3 hours ago   Up 7 minutes             festive_stonebraker

> docker ps -a
CONTAINER ID   IMAGE     COMMAND   CREATED       STATUS                      PORTS     NAMES
f6ac9aab4464   ubuntu    "bash"    3 hours ago   Up 5 minutes                          festive_stonebraker
2a15a37e42ae   ubuntu    "bash"    3 days ago    Exited (255) 43 hours ago             wonderful_villani
ef7b81a5eb47   ubuntu    "bash"    3 days ago    Exited (255) 3 days ago               cranky_villani
```

---

## Run a container locally.

Commands:

> - docker run [--name \<containerName>] [-it] [-d] [-p \<hostPort> \<containerPort>] [-e \<variableName>=\<variableValue>] [-v \<volumeName:containerAbsolutePath>] \<imageName>

Notes:

> - -it for interactive mode so you can work with it.
> - -d for detached mode, it works in background.
> - -p for publishing a port on host.
> - -v for setting a valoume to the image.
> - -e for setting eviroment variables.

Example:

```
> docker run -it ubunto
> docker run -it react-app:4 sh
> docker run -d react-app:5
> docker run -d --name hello react-app:4
> docker run -d -p 80:3000 --name hello react-app:4
> docker run -d -p 80:3000 -v app-data:/app/data react-app:5
> docker run -p 5000:5000 -e ASPNETCORE_HTTP_PORT=http://+:5000 -e ASPNETCORE_URLS=http://+:5000 docker-sample:4
```

---

## Start a container

Commands:

> - docker start \<containerId>

Example:

```
> docker start 2fa
```

---

## Stop a running container.

Commands:

> - docker stop \<containerId>

Example:

```
> docker stop 2fa
```

---

## Remove a container.

Commands:

> - docker container rm \<containerId> [--force]

Notes:

> - --force Removes the container even if it's running.

Example:

```
> docker container rm 2fa
> docker container rm 2fa --force
```

---

## Execute a command on running container

Commands:

> - docker exec [-it] [-u \<username>] \<containerId> \<command>

Note:

> - -it is for interactive mode.
> - -u is for username.

Example:

```
> docker exec -it -u john 2fa bash
> docker exec -it 2fa sh
```

---

## Build an image

Commands:

> - docker build [-t \<tagname>[:\<tag>]] \<dockerFileLocation>

Note:

> - -t is for tag name.
> - . means current directory.

Example:

```
> docker build -t react-app .
> docker build -t react-app:1.2.5 .
```

---

## Delete all stoped containers.

Commands:

> - docker container prune

Example:

```
> docker container prune
```

---

## Delete all dangling images

Commands:

> - docker image prune

Note: No container should use these images.

Example:

```
> docker image prune
```

---

## Delete an image.

Commands:

> - docker image rm \<imageId>

Example:

```
> docker image rm df3
```

---

## Remove a tag from an image

Commands:

> - docker image remove \<imageName>:\<tag>

Example:

```
> docker image remove react-app:1.2.5
```

---

## Change an image tag.

Commands:

> - docker image tag \<imageName>:\<tag> \<newImageName>:\<newTag>

Example:

```
> docker image tag react-app:latest react-app:1.2.5
```

---

## Save an image locally.

Commands:

> - docker image save --output \<path> \<imageName>

Example:

```
> docker image --output react-app.tar react-app:1.2.5
```

---

## Load an image locally

Commands:

> - docker image load --input \<path>

Example:

```
> docker image --input react-app.tar
```

---

## Shows container's log.

Commands:

> - docker logs [--follow] [--tail \<lineNumber>] [--timestamps] \<containerId>

Notes:

> - --follow for live logs.
> - --tail for showing last n lines.
> - --timestamps for showing the log times.

Example:

```
> docker logs 655
> docker logs --follow 6d3aedcb
> docker logs --tail 5 6d3aedcb6169
> docker logs --tail 3 --timestamps 6d3aedcb6169
```

---

## Create a volume

Commands:

> - docker volume create \<volumeName>

Notes:

> - Volume is a storage outside of container. it can be on host ir
>   Example:

```
> docker volume create app-data
```

---

## Inspect a volume

Commands:

> - docker volume inspect \<volumeName>

Example:

```
> docker volume inspect app-data
```

---

## Copy files.

Commands:

> - docker cp \<sourcePath> \<destinationPath>

Notes:

> - You can combine containerId with absolute path for source or destination path.

Example:

```
> docker cp file.txt ec1:/app
> docker cp ec1:/app/file.txt .
```

---

## Mounting a folder to container

Commands:

> - docker run -v \<localPath>:\<containerPath> \<containerId>

Example:

```
> docker run -d -p 5001:3000 -v $(pwd):/app react-app:5
```

---

# Creating Dockerfile

## Containerizing ASP.NET Core
1. Open the project in VsCode
2. Install Docker Extension for VsCode
3. Press ctrl + shift + P to open the command pallete and run this command:
    - Dcocker: Add Docker Files to workspace
    - Application platform: ASP.NET Core
    - Operation system: Linux
    - Listening port: 5000
4. Open terminal and navigate to the project path and run the build command:
    - docker build -t dockersample:v1 .
5. In the terminal run the run command:
    - docker run -p 5000:5000 -e ASPNETCORE_HTTP_PORT=http://+:5000 -e ASPNETCORE_URLS=http://+:5000 dockersample:v1
