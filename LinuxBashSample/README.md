# Linux Common Commands
| Command | Sample | Description |
| --- | --- | --- |
| echo | echo "hello" | Display text or the value of variables in the terminal |
| echo | echo "hello" > test.txt | Writes the text to a file (overwrite the file) |
| echo | echo "hello" >> test.txt | Appends the text to a file |
| man | man ls | Show the command manual |
| man | man ls | Show the command manual |
| whoami | whoami | Shows the current user |
| env | env | Shows environment variables |
| ps -ef | ps -ef | Show running processes |
| kill [id] | kill 6920 | Kill a process |
| id | id | Shows the current user uid, gid |
| su | su | Switch to the root user |
| sudo | su rm file.txt | Runs the command with root privilege. |
| pwd | pwd | Shows the current path |
| su - username | su - test | login with a username |
| ssh | ssh -p 2220 username@hostAddress | Connect via ssh |
| ssh -i | ssh -i C:\temp\sshkey.private user@hostaddress -p 2220  | Connect via ssh an private key |
| ls | ls | list files and directories |
| ls -l | ls -l | list files and directories with their permissions. |
| grep | grep "word" filename.txt | Text searching utility that allows you to search through files or output for specific patterns. |
| sed | sed -i 's/some/many/g' file.txt | Manipulating text streams. |
| more | more largefile.txt | view the contents one screen at a time. |
| stat | stat file.txt | Shows detailed information about the files. |
| cat | cat file.txt | Concatenate files and print on the standard output |
| cat /etc/shells | cat /etc/shells | Shows the list of shells. |
| nano | nano file.txt | Text editor |
| vi | vi file.txt | Open a text in Visual Editor |
| file | file example.txt |  determine the type of a file |
| du | du file.txt | Estimate file space usage |
| find | find [path] [expression] -exec [command] {} + | Find files or directories |
| reset | ??? | ??? |
| sort yourfile.txt | sort sample.txt | Sort the content |
| uniq | uniq -u | ? |
| strings | strings data.txt | extract printable strings from binary files. |
| base64 | base64 --decode data2.txt | Decode and Encode in Base64  |
| tr | tr 'A-Za-z' 'N-ZA-Mn-za-m' < data3.txt | way to perform ROT13 decoding. |
| gzip | ??? | compress or expand files |
| bzip2 | ??? | a block-sorting file compressor |
| xxd | ??? | make a hex dump or do the reverse. |
| tar | tar -xvf data4 | To extract the contents of a tar archive |
| cp | ??? | Copy a file |
| mv | ??? | Move or rename |
| mkdir | ??? | Make Directory |
| touch | touch file.txt | create a file |
| mktemp | ??? | Make a temp directory |
| chmod | chmod 777 | Change the permissions |
| chown | chmod testuser file.txt | Change the owner |
| chgrp | chgrp testuser file.txt | Change the group |
| nc | nc localhost 30000 | Creating a network connection (NetCat) |
| nc -zv | nc -zv localhost 500-600 | Port Scanning (NetCat) |
| nc -l | nc -l 2220 | Listening to a port |
| nc -l port | nc -l 2220 > received_file | Receive a file |
| nc hostAddress port | nc localhost 2220 < file to send | Send a file |
| ncat | ncat --ssl 192.168.1.1 31421 | Connect or listen with SSL |
| opens ssl | openssl s_client -connect localhost:30000 | OpenSSL is a toolkit that implements the (SSL) and (TLS) protocols. |
| nmap | nmap localhost -p 31000-32000 | Scan the ports. |
| nmap | nmap 192.168.1.1-254 | Scan IP range. |
| diff | diff file1.txt file2.txt | Compare values. |
| diff -u | diff -u file1.txt file2.txt | Compare values in unified format. |
| diff -y | diff -y file1.txt file2.txt | Compare values side by side. |
| diff -y --suppress-common-lines | diff -y --suppress-common-lines file1.txt file2.txt | Compare values side by side. (only the differences) |


## "|", ">", and "<"
...

## File System
...
### / (Root)
...
### /home
...
### /tmp
...
### /boot
...
### /dev
...
### /etc
...
### /etc/passwd
...
### /var
...
### /bin
...
### /sbin
...

## Environment Variables
Environment variables in Linux are **dynamic** values that affect the behavior of processes and applications on the system. They are used to store information that the operating system and various programs need to operate properly.

### Common Environment Variables:
- **PATH**: A list of directories where the system looks for executable files.
- **HOME**: The current user's home directory.
- **USER**: The username of the current user.
- **SHELL**: The path to the current shell being used.
- **LANG**: The system's language and locale settings.
- **PWD**: The current working directory.

```bash
# Print all environment variables
env

# Print a specific environment variables
printenv HOME
# Or
echo $HOME
# Or
$HOME
```

### Temporary variables.
We can set or create a **temporary** environment variable with export command.<br>
And with **unset** command we can remove them.
```bash
# Create a temporary variable and set the value to Test
export MYVAR=Test

# Print the created variable
echo $MYVAR

# Change the value of SHELL temporary.
export SHELL="/bin/newbash"

# Remove a variable.
unset MYVAR
``` 
### Permanent Variables. 
To set an environment variable permanently, we would typically add it to one the  configuration files like 
- ~/.bashrc (for current user)
- ~/.bash_profile (for current user)
- /etc/environment (for all users)

We just need to open them in a text editor like nano or vim and add our configuration at the end of the file.
```bash
export MYVAR="This is the values!"
```
Now save the file and run the **source** command to update the reload the configuration file and update the variables.
```bash
source ./bashrc
```

## Daemons
...

## CronJob and CronTab

create sample: 
```bash
./my_script.sh &
```
...

## chmod and file permissions
The **chmod** is used to change the file system **modes** of files and directories. This includes modifying the **read**, write**, and **execute** permissions.

### Changing Permissions Using Symbolic Notation
The symbolic notation uses **u, g, o, and a** to represent **user (owner)**, **group**, **others**, and **all users**, respectively. It also uses **+, -, and =** to **add, remove, or set** permissions.

- u: user (owner)
- g: group
- o: others
- a: all (user, group, and others)
>
- r: read
- w: write
- x: execute

```bash
# Added read, write, execute permission to all users.
chmod a+rwx filename.txt

# Removed read and write permission from the owner.
chmod u-rw filename.txt

# added read permission to the group.
chmod g+r filename.txt

# Added execute permission to other users.
chmod o+x filename.txt

# Set read and write  permission to all users.
chmod a=rw filename.txt

# We can Also combine the commands.
chmod u+r,g+w,o+x filename.txt 
```

###  Changing Permissions Using Numeric (Octal) Notation
In octal notation, permissions are represented by a **three-digit number** where each digit ranges from **0 to 7**. Each digit represents a set of permissions for the **user, group, and others**, respectively.
- r (read) = 4
- w (write) = 2
- x (execute) = 1

Each permission set is the sum of its individual permissions:
- 7 (rwx) = 4 + 2 + 1
- 6 (rw-) = 4 + 2
- 5 (r-x) = 4 + 1
- 4 (r--) = 4
- 3 (-wx) = 2 + 1
- 2 (-w-) = 2
- 1 (--x) = 1
- 0 (---) = 0

```bash
# All users have Read, Write and execute permission.
chmod 777 filename.txt 

# All no permission.
chmod 000 filename.txt 

# Owner read, write execute - group, read and write - others, none
chmod 760 filename.txt 
```

### Changing Special Permissions
The chmod command can also set special permissions such as **setuid**, **setgid**, and the **sticky bit** using symbolic or octal notation.

**SetUID**: File will be run withe owner rights instead of current user's right.

**SetGID**: The group owner of the directory would be applied on all the content of the directory.

**Sticky Bit**: Only owner of the file/directory can remove the it.

```bash
# Set the setuid bit
chmod u+s file

# Set the setuid bit using octal notation (4 is for UID)
chmod 4755 file

# Set the sticky bit
chmod +t directory

# Set the sticky bit using octal notation (1 is for Sticky Bit)
chmod 1755 directory

# Set the setgid to a directory
# Assign the current user as the  group owner of the directory 
chmod g+s directory

# Set the setgid to a directory using octal notation (2 is for )
chmod 2755 directory
```

## Remove (rm)
| Command and Option | Description |
| --- | --- |
| rm -d | Remove an empty directory |
| rm -r | Remove a directory and its content. |
| rm -f | Ignore any prompt when deleting a write-protected file. |
| rm -i | Prompt before every removal |
| rm -I | Prompt once before removing more than three files |
| rm * | Wildcard that represents multiple characters. |
| rm ? | Wildcard that represents a single character. |
| rmdir -p | Remove an empty subdirectory and its parent directory. |
| rmdir -v | Print the information that the specified directory was deleted. |

## sed
**sed** is a powerful command-line tool used for manipulating text streams. It's often referred to as a stream editor because it processes text line by line. Common operations include:   

- Finding and replacing text
- Deleting lines   
- Inserting lines   
- Appending text

Example
```bash
# needs explanation
sed -i 's/some/many/g' file.txt
```

## locate
The **locate** command in Linux is used to quickly find files and directories on the system. It searches a pre-built database (mlocate.db) that contains an index of all the files and directories on the system, making it much faster than the find command, which searches the file system in real-time.

```bash
# Update the database
sudo updatedb

# Find files by name
locate filename

# Search with a wildcard
locate *.conf

# locate -i readme
locate -i readme
```

## Transferring Files using SSH  

```bash
scp -P 22 root@hostaddress:~/target.txt C:\temp
```
If you need to authenticate via private key
```bash
scp -i key_file.pem your_username@remotehost.edu:/remote/dir/foobar.txt /local/dir
```

### OR using SSH
```bash
 ssh username@hostaddress -p 2220 'cat temp.txt' > C:\temp\temp.txt
 ```

## Curl command
...


## Nmap
Nmap (Network Mapper) is an open-source tool used for network discovery and security auditing.<br>
Nmap can be used for scanning and discovering networks, hosts, IP ranges, ports, services and vulnerabilities.

### common switches.
| Switch | Description |
| --- | --- |
| -sn | Ping Scan - Disable port scan |
| -sT | TCP Scan - Full open scan, it uses the 3-way handshake. |
| -sU | UDP Scan |
| -sL | DNS Scan (List scan) |
| -sS | Stealth scan - Half open scan - Broken TCP without ack packages |
| -p <port ranges> | Only scan specified ports |
| -S <IP_Address> |  Spoof source address |
| -O | Enable OS detection |
| -T<0-5> | Set timing template (higher is faster) |
| -A | Enable OS detection, version detection, script scanning, and traceroute |
| -D <decoy1,decoy2[,ME],...> | Cloak a scan with decoys |

### Some Examples
| Command | Description |
| --- | --- |
| nmap localhost -p 31000-32000 | Scan a ports range. |
| nmap 192.168.1.1-254 | Scan an IP range. |
| nmap -sn 192.168.0.0/24 | Scan a network. |
| nmap -sS -D 192.168.0.1-50 | Scan a network in stealth mode and with decoy. |

### Nmap Scripting Engine (NSE)
Nmap custom scripting involves writing or using scripts with the Nmap Scripting Engine (NSE) to perform custom or advanced network scans and operations. These scripts can be used to detect vulnerabilities, gather additional information about the target, or automate a series of Nmap commands.<br>

NSE scripts are written in the **Lua** programming language, which is lightweight and highly extensible.<br>

NSE provides a range of libraries that simplify common tasks such as making HTTP requests, parsing XML, handling cryptographic functions, etc.

Nmap organizes NSE scripts into categories such as:
- **auth**: Scripts related to authentication bypass or checks.
- **brute**: Scripts that perform brute-force attacks.
- **vuln**: Scripts that check for vulnerabilities.
- **discovery**: Scripts that discover more information about the target.
- **safe**: Scripts that are non-intrusive and safe to run on production systems.

List of the categories and the scripts can be found in https://nmap.org/nsedoc/categories/

We can update the list scripts database by running this command
```bash
sudo nmap --script-updatedb
```

We can also run all all the scripts inside a category like this
```bash
# sudo nmap --script <Category name> <target>
sudo nmap --script vuln 127.0.0.1
```

Users can write their own custom scripts to fulfill specific needs. These scripts can be shared or reused across different environments.

We can run the scripts with --script option
```bash
nmap --script script1,script2,script3 --script-args 'arg1=value1,arg2=value2' <target>
```
---

## Subfinder
Subfinder is an open-source tool designed for automating the process of discovering subdomains of a given domain.<br>

It primarily performs **passive subdomain enumeration** by querying multiple online sources, including DNS services, certificate transparency logs, web archives, and more. This approach reduces the risk of detection since it doesn't actively probe the target domain.

 It can output results in various formats, such as JSON or CSV, which makes it easy to integrate with other tools or scripts for further analysis.

you can download it from their github page https://github.com/projectdiscovery/subfinder
>This tool should only be used if you have obtained the target's written consent.
 ```bash
subfinder -d domanName,domainName2,domainName3
 ```
 

<br>
<br>
<br>
<br>
<br>

# Shells and Bash Scripting
Here's a list of the most common Linux shells:

## Bourne-based Shells
- **Bourne Shell (sh)**: The original shell, still used for scripting.
- **Bourne Again Shell (bash)**: The most widely used shell, offering advanced features and compatibility.
- **Korn Shell (ksh)**: Known for its powerful scripting capabilities and interactive features.
- **Debian Almquist Shell (dash)**: A lightweight and efficient shell often used in embedded systems.

## C-based Shells
- **C Shell (csh)**: Less common now, but still used in some environments.
- **TENEX C Shell (tcsh)**: An enhanced version of csh with additional features.

## Modern Shells
- **Z Shell (zsh)**: Combines features from bash, ksh, and tcsh, offering extensive customization and plugins.
- **Fish (Friendly Interactive Shell)**: User-friendly with syntax highlighting, autosuggestions, and other interactive features.

 We can find he available shells here.
 ```bash
cat /etc/shells
 ```
 ---
 
 A Bash script is a text file with **.sh** extension containing a sequence of commands that are executed by the Bash shell.<br/>

 Most Bash scripts begin with a "shebang" **#!** line, which indicates the script should be executed with Bash. The shebang line looks like this:
 ```bash
 #!/bin/bash
 ```

A Bash script can contain any command that you would normally type into a Bash terminal. This includes commands like ls, echo, cd, and more complex operations using loops, conditionals, and functions.
```bash
#!/bin/bash
echo "Hello, World!"
```

We can run a bash script file by the **bash** command
```bash
bash my_script.sh
```

## Loops
There are 3 kind of loops that we can use. (for, while and until)

### for loop
The for loop iterates over a list of items, executing the block of code for each item.
```bash
#!/bin/bash

# Iterating List of files
for file in file1.txt file2.txt file3.txt
do
    echo "Processing $file"
    # Add commands to process the file here
done

#----------------------------------------------
# Loop through numbers 1 to 5
for i in {1..5}
do
    echo "Iteration $i"
done

#----------------------------------------------
# C-style for loop
for ((i=1; i<=5; i++))
do
    echo "Iteration $i"
done
```

### while loop
The while loop continues to execute as long as the specified condition is true.
```bash
#!/bin/bash

counter=1

while [ $counter -le 5 ]
do
    echo "Counter is at $counter"
    ((counter++))
done

#----------------------------------------------
# Read a file line by line
while IFS= read -r line
do
    echo "Line: $line"
done < "file.txt"
```

### until loop
The until loop is the opposite of the while loop; it continues to execute until the specified condition is true.
```bash
#!/bin/bash

counter=1

until [ $counter -gt 5 ]
do
    echo "Counter is at $counter"
    ((counter++))
done
```
We can use the **break** statement to exit a loop prematurely and the **continue** statement to skip the rest of the current iteration and move to the next iteration.
```bash
#!/bin/bash

for i in {1..10}
do
    if [ $i -eq 5 ]; then
        echo "Skipping iteration $i"
        continue  # Skip the rest of this iteration
    fi
    
    if [ $i -eq 8 ]; then
        echo "Breaking the loop at iteration $i"
        break  # Exit the loop
    fi
    
    echo "Iteration $i"
done
```

## Positional Parameters
Positional parameters are placeholders for values that you provide when running a script or command.<br>
They are **ordered**, meaning the first value you provide corresponds to the first parameter, the second value to the second parameter, and so on.<br>
They are referenced by numbers, starting from **1** (e.g., $1, $2, $3).<br>
**$0** is a special parameter that holds the name of the script itself.<br> 
You can access more than 9 positional parameters using curly braces (e.g., \${10}, \${11}).

Example<br>
If we create a script.sh with the following content.
```bash
#!/bin/bash
echo Name of the script is $0
echo Hello $1
echo Hello $2
echo Hello $3
echo Hello $4
echo Hello $5
echo Hello $6
echo Hello $7
echo Hello $8
echo Hello $9
echo Hello ${10}
echo Hello ${11}
```
You can run this run script and pass the arguments like this
```bash
bash script.sh John Jane Peter Jackson Jim Sara Kate Martin Ana Alice Jack
```
The result should be like this
```text
Name of the script is script.sh
Hello John
Hello Jane
Hello Peter
Hello Jackson
Hello Jim
Hello Sara
Hello Kate
Hello Martin
Hello Ana
Hello Alice
Hello Jack
```