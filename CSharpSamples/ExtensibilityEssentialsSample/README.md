# Extensibility Essentials 2022 Sample
**Extensibility Essentials** is a collection of extensions designed to make it easier for developers to write Visual Studio extensions. It provides a set of tools and features that simplify the process of creating custom extensions within Visual Studio.

---

## Installation and Configuration 
1. Open Visual studio installer (Tools -> Get Tools and Features) and install **Visual Studio extension development**.
2. Go to Extensions -> Manage extensions and Install **Extensibility Essentials 2022** extension in Visual studio.
3. Create a new **VSIX Project** project.
4. Now we can set the Meta data of our extension in **source.extension.vsixmanifest** file.
5. Right click on the project and create a new item, select **command** and create.
<br>By doing, beside the **{CommandName}Command.cs**, another file will be created named **{ProjectName}Package.vsct**.<br>
In the **{CommandName}Command.cs** file, inside the **Execute** method we can define the logic of the button<br>

    ```C#
    private void Execute(object sender, EventArgs e)
    {
        ThreadHelper.ThrowIfNotOnUIThread();
        string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
        string title = "This is my title";

        // Show a message box to prove we were here
        VsShellUtilities.ShowMessageBox(
            this.package,
            message,
            title,
            OLEMSGICON.OLEMSGICON_INFO,
            OLEMSGBUTTON.OLEMSGBUTTON_OK,
            OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
    }
    ```

    In **source.extension.vsixmanifest** file we can setup the extensions UI. like the location of the buttons.
    ```xml
    <Groups>
        <Group guid="guidVSIXProjectPackageCmdSet" id="MyMenuGroup" priority="0x0600">
            <Parent guid="guidSHLMainMenu" id="IDM_VS_MENU_EDIT" /> <!-- Groupd is defined in the VS Edit menu -->
        </Group>
        <Group guid="guidVSIXProjectPackageCmdSet1" id="MyMenuGroup" priority="0x0600">
            <Parent guid="guidSHLMainMenu" id="IDM_VS_MENU_TOOLS" /> <!-- Group is defined in the VS Tools menu -->
        </Group>
    </Groups>

    <Buttons>
      <Button guid="guidVSIXProjectPackageCmdSet" id="Command1Id" priority="0x0100" type="Button">
        <Parent guid="guidVSIXProjectPackageCmdSet" id="MyMenuGroup" />
        <Icon guid="guidImages" id="bmpPic1" />
        <Strings>
          <ButtonText>Invoke Saman</ButtonText>
			<LocCanonicalName>.Edit.guidVSIXProjectPackageCmdSet</LocCanonicalName>
        </Strings>
      </Button>
      <Button guid="guidVSIXProjectPackageCmdSet1" id="cmdidInsertGuidCommand" priority="0x0100" type="Button">
        <Parent guid="guidVSIXProjectPackageCmdSet1" id="MyMenuGroup" />
        <Icon guid="guidImages1" id="bmpPic1" />
        <Strings>
          <ButtonText>Invoke InsertGuidCommand</ButtonText>
        </Strings>
      </Button>
    </Buttons>
    ```

---
