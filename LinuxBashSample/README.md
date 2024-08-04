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
| chmod | ??? | change file mode bits |



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

## Download Files using SSH 

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