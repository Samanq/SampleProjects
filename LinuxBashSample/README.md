| Command | Sample | Description |
| --- | --- | --- |
| man | man ls | Show the command manual |
| ssh | ssh -p 2220 username@hostAddress | Connect via ssh |
| ssh -i | ssh -i C:\temp\sshkey.private user@hostaddress -p 2220  | Connect via ssh an private key |
| ls | ls | list files and directories |
| cat | ??? | Concatenate files and print on the standard output |
| nano | ??? | Text editor |
| file | file example.txt |  determine the type of a file |
| du | ??? | Estimate file space usage |
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
| mktemp | ??? | Make a temp directory |
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







## chmod
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
```bash
# Set the setuid bit
chmod u+s file

# Set the setuid bit using octal notation
chmod 4755 file

# Set the sticky bit
chmod +t directory

# Set the sticky bit using octal notation
chmod 1755 directory
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