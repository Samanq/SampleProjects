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
1. Open Terminal and run:
	winget install JanDeDobbeleer.OhMyPosh -s winget
2. Go to https://www.nerdfonts.com/ and download a font (**Meslo LGM NF** recommended!).
3. Extract and install the font.
4. After installing the font open terminal settings ba pressing (CTRL + SHIFT + , ) and add this section:
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

5. Restart the terminal and run this command to open profile:
```poweshell
code $profile
```
	
6. add this line to profile and save it, after that restart the terminal
```json
oh-my-posh init pwsh --config "$env:POSH_THEMES_PATH\jandedobbeleer.omp.json" | Invoke-Expression
```
---

## Installing Terminal Icons:
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

Installing Posh-Git: 
1. open powershell
2. Write Install-Module posh-git -Scope CurrentUser
3. Write Install-Module oh-my-posh -Scope CurrentUser
4. Write code $profile
5. in the profile write these lines
	Import-Module posh-git
	Import-Module oh-my-posh
	Set-PoshPrompt -Theme cloud-native-azure						=> (you can find more theme in: https://ohmyposh.dev/docs/themes)
6. Restart the powershell
7. Go to https://www.nerdfonts.com/ and download a font				=> (Caskaydia Cove Nerd Font. recommended!)
8. Install the font.												=> (Or just extract them in C:\Windows\fonts)
9. Open powershell option and change font to: CaskaydiaCove NF
10. Restart the powershell.

For changing VSCode powershell:
1. Open VsCode Terminal, prees plus and select powershell. (now your terminal is powershell)
2. Go to file -> Preferences -> Settings. and search for font.
3. At the Editor: Font Family and the font name to end of it 		=> (, CaskaydiaCove NF)

For Changig VisualStudio PowerShell:
1. Open Visual Studio
2. Go to View -> Terminal
3. Click on Setting icon
4. Click on Add
5.  Name = PowerShell, 
	Shell Localtion = C:\Program Files\WindowsApps\Microsoft.PowerShell_7.2.2.0_x64__8wekyb3d8bbwe\pwsh.exe (your powershell location)
	Argument =
6. Go to Fonts and colors 
7. Select Terminal
8. for the font select CaskaydiaCove NF
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Auto Complete:
code $PROFILE
Append this code:
#-------------------------------- Auto Complete --------------------------------
Import-Module PSReadLine

# Shows navigable menu of all options when hitting Tab
Set-PSReadLineKeyHandler -Key Tab -Function MenuComplete

# Autocompleteion for Arrow keys
Set-PSReadLineOption -HistorySearchCursorMovesToEnd
Set-PSReadLineKeyHandler -Key UpArrow -Function HistorySearchBackward
Set-PSReadLineKeyHandler -Key DownArrow -Function HistorySearchForward

Set-PSReadLineOption -ShowToolTips
Set-PSReadLineOption -PredictionSource History
#------------------------------ End Of Auto Complete ------------------------------
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Configuration:
	Bash Commands:
git config --global user.name "Saman Qaydi"					=> Set default name
git config --global user.email saman.qaydi@live.com			=> Set default email
git config --global core.editor "code --wait"				=> set default editor, (--wait  tells the terminal to wait until we close VsCode instance)
git config --global -e										=> Get configuration
git config --global core.autocrlf true						=> Handle end of lines (Windows)
git config --global core.autocrlf input						=> Handle end of lines (Windows) (Mac || Linux)
git config --global diff.tool vscode						=> set vscode for default diff tools

git config --global difftool.vscode.cmd	"code --wait --diff $LOCAL $REMOTE"		=> set vscode for default diff tools
git config --global alias.lg "log --pretty=format:'%an committed %h'"			=> define custom command

git config --global merge.tool p4merge											=> Set the default merge tool to p4merge.
git config --global mergetool.p4merge.path "c:\program files..."				=> Define p4merge application path

git config --help											=> Full help
git config --h												=> short help
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
git init												=> initialize the git in current folder
git status												=> get status 
git status -s											=> get status short
git add fileName.txt									=> add file to stagging erea
git add fileName1.txt fileName2.txt						=> add files to stagging erea
git add *.txt											=> add all txt files to stagging erea.
git add .												=> add entire directory to stagging erea.
git commit -m "Description"								=> commit in repository (-m is for message).
git commit -a											=> commit all modified files into the  repository (-a is for all).
git commit -am											=> commit all changed (-am is -a -m combined together).
git commit												=> commit in repository
git commit --amend										=> apend to last commit 
git ls-files											=> Get list of files in staggin area
git rm file.txt											=> remove file from directory and stagging area
git rm --cached file.txt								=> remove file only from stagging area (staging area = index)
git rm --cached -r bin/									=> remove directory only from stagging area (staging area = index)
git mv main.js file1.js									=> rename file and add to stagging area
echo logs/ > .gitignore									=> create .gitignore file and add logs folder to it for ignoring this folder
git diff --staged										=> show thte difference in staged area
git diff												=> get on staged differences
git difftool											=> get diferences with vscode
git diff master-otherBranchName							=> Show the diffrences in two branches
git log													=> show all commits
git log --oneline										=> show all commits short version
git log --oneline --reverse								=> show all commits short version in reverese order
git log --oneline --stat								=> show all commits an changes in detail
git log --stat											=> show all commits an changes in detail
git log --oneline --patch								=> show all commits an changes in detail
git log --oneline --author="Saman"						=> show all commits filter by author
git log --oneline --before="2020-08-17"					=> show all commits filter by Date before
git log --oneline --after="2020-08-17"					=> show all commits filter by Date after
git log --oneline --after="yesterday"					=> show all commits filter by Date after yesterday
git log --oneline --after="one week ago"				=> show all commits filter by Date after one week ago
git log --oneline --grep="Something"					=> show all commits filter search keyword (kley sensetive)
git log --oneline -S="hello()"							=> show all commits that add or remove this line
git log --pretty=format:"hello %an"						=> show all commits with format
git show commitID										=> Show Changes in commit
git show HEAD											=> Show Changes in last commit
git show HEAD~1											=> Show Changes in 1 previous commit
git ls-tree commitId									=> Show all the files and directories in commit
git restore --staged file1.js							=> Undo changes on file1.js
git restore file1.js									=> Undo local changes on file1.js
git clean -fd											=> Remove all untracked files (f: force, d: directories)
git restore --source=commitId file1.js					=> Restore a file to specified commit version
git checkout commitId									=> Restore to a commit
git checkout master										=> attack head pointer to master branch
git checkout -- fileName								=> Undo unstaged changes
git checkout -- .										=> Undo all unstaged changes
git checkout branchName									=> Get romote branch ans switch to it
git bisect start										=> start bisect
git bisect bad
git bisect good commitId
git bisect reset
git shortlog
git blame file.txt										=> Findig who write that code
git tag name commitId									=> add a tag to a commit
git tag -d tagName										=> delete a tag
git branch name											=> Create a branch
git branch												=> Get branches
git switch BranchName									=> Switch Branch
git switch -C branchName								=> Create a new branch and switch to it.
git branch -m oldBranchName newBranchName				=> Rename Branch
git branch -d branchName								=> Delete Branch
git branch -D branchName								=> Delete Branch By force, even if it's not merge
git log master..otherBranchName							=> Show all the commits that are in the otherBranches and not in master
git stash push -m "Some message"						=> Stash 
git stash push -am "Some message."						=> Stash all.
git stash list											=> Show stash list
git stash show stash@{1}								=> Show changes in stash. OR
git stash show 1										=> Show changes in stash.
git stash apply 1										=> Apply stash 
git stash drop 1										=> remove stash
git stash clear											=> remove all stashes
git merge branchName									=> merge branche to master with fastforward method.
git merge --no-ff branchName							=> merge with no fastforward. (3 way merge)
git merge --abort										=> Abort the merge.
git mergetool											=> Open MergTool for merging. (We have to install and set the default merge tool)
git reset --hard HEAD~1									=> Undo Merge
git revert HEAD											=> Rvert the last commit
git revert -m 1 HEAD									=> Rvert the last commit (m 1 means first parent)
git clone url											=> Clone a repository
git remote												=> List of remote repository
git fetch [RepositoryName] [branchName]					=> Fetch Repository
git branch -vv											=> shows local and remote branches diverging
git branch -r											=> show remote branches
git pull												=> pull repository and merge
git pull rebase											=> pull with rebase
git push [Repositoty] [branch]							=> Push to remote repository
git push -f												=> push with force
git push origin tagName									=> push to to remote repository
git push origin --delete tagName						=> delete tag from remote repository.
git push -u [remoteRepository] [LocalBranch]			=> push localBranch to remote repository
git push -d [remoteRepository] [LocalBranch]			=> delete a branch from remote repository.
git reflog												=> how refrences move in our history
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Unix Bash Commands:
pwd							=> Show current directory
cd d:/						=> change location
ls							=> list directory
ls -a						=> list directory with hidden files
mkdir Name					=> Create Directory.					
rm Name						=> Remove Directory.					
rm -r Name					=> Remove Directory Recursivly.					
rm file.txt					=> Remove File.					
echo hello > file1.txt		=> wtire something inside txt file
echo hello >> file1.txt		=> append something inside txt file
nano file.txt				=> edit text with text editor.
vim file.txt				=> open file in vim editor.
clear						=> clear the screen.
mv file1.txt main.js		=> Rename file 
cat file.txt				=> Read a txt file.
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
vim command:
Save File and Exit:
1. press Esc 
2. type :wx
3. Press Enter				=> Save and Exit
-------------------------------------------------------
Exit without Save:
1. press Esc 
2. type :q! 
3. Press Enter				=> Exit without save
