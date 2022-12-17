# Git Sample


## Git extensions for vscode
- Gitlens
- Git Graph	(Showing branches in graphical form)

## Git Tools
- GitKraken and SourceTree (Git Client UI)
- Kdiff, P4Merge, WinMerge (Merge Tools)
---

## Installing Git:
Go to http://git-scm.com/ for download latest version of git.

---
## Installing Oh-My-Posh on Terminal
1. Open terminal as administrator and run this command
```powershell
Set-ExecutionPolicy RemoteSigned
```
2. Open Terminal and run this command
```powershell
winget install JanDeDobbeleer.OhMyPosh -s winget
```
3. Go to https://www.nerdfonts.com/ and download a font (**Meslo LGM NF** recommended!).
4. Extract and install the font.
5. After installing the font open terminal settings ba pressing (CTRL + SHIFT + , ) and add this section:
```json
{
    "profiles":
    {
        "defaults":
        {
            "font":
            {
                "face": "MesloLGM NF"
            }
        }
    }
}
```

6. Restart the terminal and run this command to open profile:
```poweshell
code $profile
```
	
7. add this line to profile and save it, after that restart the terminal
```powershell
oh-my-posh init pwsh --config "$env:POSH_THEMES_PATH\jandedobbeleer.omp.json" | Invoke-Expression
```
---

## Installing Terminal Icons
1. open terminal as administrator and run this command
```powershell
Install-Module -Name Terminal-Icons -Repository PSGallery
```
2. Run this command to open profile
```powershell
code $profile
```
3. Add this line to the **profile** and save it, after that, restart the terminal
```json
Import-Module -Name Terminal-Icons
```
---

## Enable Intellisense
1. Open terminal and run this command
```powershell
Install-Module PSReadLine -force
```
	
2. Run this command to open profile
```powershell
code $profile
```
	
3. Append this line to the **profile** and save it, after that restart the terminal
```json
Import-Module PSReadLine
Set-PSReadlineOption -PredictionSource History
```

> Note: if you want to disable it you can run this command
```powershell
Set-PSReadlineOption -PredictionSource None
```
---

## Git Configuration

Bash Commands:
| Command | Description |
| --- | --- |
| git config --global user.name "Saman Qaydi"| Set default name |
| git config --global user.email saman.qaydi@gmail.com | Set default email |
| git config --global core.editor "code --wait"| set default editor, (--wait  tells the terminal to wait until we close VsCode instance) |
| git config --global -e | Get configuration |
| git config --global core.autocrlf true | Handle end of lines (Windows) |
| git config --global core.autocrlf input | Handle end of lines (Windows) (Mac or Linux) |
| git config --global diff.tool vscode | set vscode for default diff tools |
| git config --global difftool.vscode.cmd	"code --wait --diff $LOCAL $REMOTE" | Set vscode for default diff tools |
| git config --global alias.lg "log --pretty=format:'%an committed %h'" | define custom command |
| git config --global merge.tool p4merge | Set the default merge tool to p4merge. |
| git config --global mergetool.p4merge.path "c:\program files..." | Define p4merge application path |
| git config --help | Full help |
| git config --h | short help |

---

## Git Commands
| Command | Description |
| --- | --- |
| git init | Initialize the git in current folder |
| git status | Get status |
| git status -s | Get status short version |
| git add fileName.txt | Add this file to stagging area |
| git add fileName1.txt fileName2.txt | Add these files to stagging area |
| git add *.txt | Add all txt files to stagging area. |
| git add . | Add entire directory with all the files that contains to stagging area. |
| git commit -m "Description" | Commit in repository (-m is for message). |
| git commit -a | Commit all modified files into the  repository (-a is for all). |
| git commit -am | Commit all changed (-am is -a -m combined together). |
| git commit | Commit in repository |
| git commit --amend | apend to the last commit  |
| git ls-files | Get list of files in staggin area |
| git rm file.txt | Remove file from directory and stagging area |
| git rm --cached file.txt | Remove file only from stagging area (staging area = index) |
| git rm --cached -r bin/ | Remove directory only from stagging area (staging area = index) |
| git mv main.js file1.js | Rename a file and add to stagging area |
| echo logs/ > .gitignore | Create .gitignore file and add logs folder to it for ignoring this folder |
| git diff --staged | Show thte difference in staged area |
| git diff | Get on staged differences |
| git difftool | Get diferences with vscode |
| git diff master-otherBranchName | Show the diffrences in two branches |
| git log | Show all commits |
| git log --oneline | Show all commits short version |
| git log --oneline --graph --all | Show all commits in graph |
| git log --oneline --reverse | show all commits short version in reverese order |
| git log --oneline --stat | show all commits and changes in detail |
| git log --stat | Show all commits and changes in detail |
| git log --oneline --patch | Show all commits an changes in detail |
| git log --oneline --author="Saman" | Show all commits filter by author |
| git log --oneline --before="2020-08-17" | Show all commits filter by Date before |
| git log --oneline --after="2020-08-17" | Show all commits filter by Date after |
| git log --oneline --after="yesterday" | Show all commits filter by Date after yesterday |
| git log --oneline --after="one week ago" | show all commits filter by Date after one week ago |
| git log --oneline --grep="Something" | show all commits filter search keyword (key sensetive) |
| git log --oneline -S="hello()" | show all commits that add or remove this line |
| git log --pretty=format:"hello %an" | show all commits with format |
| git show commitID | Show Changes in commit |
| git show HEAD | Show Changes in last commit |
| git show HEAD~1 | Show Changes in 1 previous commit |
| git ls-tree commitId | Show all the files and directories in commit |
| git restore --staged file1.js | Undo changes on file1.js |
| git restore file1.js | Undo local changes on file1.js |
| git clean -fd | Remove all untracked files (f: force, d: directories) |
| git restore --source=commitId file1.js | Restore a file to specified commit version |
| git checkout commitId | Restore to a commit |
| git checkout master | Attach head pointer to master branch |
| git checkout -- fileName | Undo unstaged changes |
| git checkout -- . | Undo all unstaged changes |
| git checkout branchName | Get romote branch and switch to it |
| git bisect start | start bisect |
| git bisect bad | .  |
| git bisect good commitId  | . |
| git bisect reset | . |
| git shortlog | . |
| git blame file.txt | Findig who write that code |
| git tagname commitId | add a tag to a commit |
| git tag -d tagName | delete a tag |
| git branch name | Create a branch |
| git branch | Display branches |
| git switch BranchName | Switch Branch |
| git switch -C branchName | Create a new branch and switch to it. |
| git branch -m oldBranchName newBranchName | Rename Branch |
| git branch -d branchName | Delete Branch |
| git branch -D branchName | Delete Branch By force, even if it's not merge |
| git log master..otherBranchName | Show all the commits that are in the otherBranches and not in master |
| git stash push -m "Some message" | Stash |
| git stash push -am "Some message." | Stash all. |
| git stash list | Show stash list |
| git stash show stash@{1} | Show changes in stash. OR |
| git stash show 1 | Show changes in stash. |
| git stash apply 1 | Apply stash. |
| git stash drop 1 | Remove stash |
| git stash clear | remove all stashes |
| git merge branchName | merge branche to master with fastforward method. |
| git merge --no-ff branchName | merge with no fastforward. (3 way merge) |
| git merge --abort | Abort the merge. |
| git mergetool | Open MergTool for merging. (We have to install and set the default merge tool) |
| git reset --hard HEAD~1 | Undo Merge |
| git revert HEAD | Revert the last commit |
| git revert -m 1 HEAD | Revert the last commit (m 1 means first parent) |
| git clone url | Clone a repository |
| git remote | List of remote repository |
| git fetch [RepositoryName] [branchName] | Fetch Repository |
| git branch -vv | Shows local and remote branches diverging |
| git branch -r | Show remote branches |
| git pull | pull repository and merge |
| git pull rebase | pull with rebase |
| git push [Repositoty] [branch] | Push to remote repository |
| git push -f | Push with force |
| git push origin tagName | Push to to remote repository |
| git push origin --delete tagName | Delete tag from remote repository. |
| git push -u [remoteRepository] [LocalBranch] | Push localBranch to remote repository |
| git push -d [remoteRepository] [LocalBranch] | Delete a branch from remote repository. |
| git reflog | How refrences move in our history |
												=> 
---

## Unix Bash Commands
| Command | Description |
| --- | --- |
| pwd | Show current directory |
| cd d:/ | change location |
| ls | list directory |
| ls -a | list directory with hidden files |
| mkdir Name | Create Directory. |
| rm Name | Remove Directory. |
| rm -r Name | Remove Directory Recursivly. |
| rm file.txt | Remove File. |
| echo hello > file1.txt | Write something inside txt file |
| echo hello >> file1.txt | append something inside txt file |
| nano file.txt | edit text with text editor. |
| vim file.txt | open file in vim editor. |
| clear | Clear the screen. |
| mv file1.txt main.js | Rename file |
| cat file.txt | Read a txt file. |

---

## vim Editor
Save File and Exit:
1. press Esc 
2. type :wx
3. Press Enter

Exit without Save:
1. press Esc 
2. type :q! 
3. Press Enter
