import { useEffect, useRef, useState } from "react";
import { useEditor, EditorContent } from "@tiptap/react";
import StarterKit from "@tiptap/starter-kit";
import Placeholder from "@tiptap/extension-placeholder";
import Image from "@tiptap/extension-image";
import Link from "@tiptap/extension-link";
import TextAlign from "@tiptap/extension-text-align";

type DirMode = "auto" | "ltr" | "rtl";

export default function ArticleEditor() {
  const [title, setTitle] = useState("");
  const [dir, setDir] = useState<DirMode>("auto"); // text direction for RTL/LTR
  const fileInputRef = useRef<HTMLInputElement | null>(null);

  const editor = useEditor({
    extensions: [
      StarterKit.configure({
        heading: { levels: [1, 2, 3] },
      }),
      Placeholder.configure({
        placeholder: "Write your article… Use / or the toolbar for formatting.",
      }),
      Image.configure({
        inline: false,
        allowBase64: true,
      }),
      Link.configure({
        autolink: true,
        openOnClick: false,
        linkOnPaste: true,
      }),
      TextAlign.configure({
        types: ["heading", "paragraph"],
      }),
    ],
    content: "",
    editorProps: {
      attributes: {
        class: "editor-content",
        dir: "auto",
      },
    },
  });

  // Update editor direction dynamically
  useEffect(() => {
    editor?.setOptions({
      editorProps: {
        attributes: {
          class: "editor-content",
          dir,
        },
      },
    });
  }, [dir, editor]);

  const setLink = () => {
    const url = window.prompt("Enter URL (leave empty to remove link):", "");
    if (!editor) return;
    if (url === null) return;

    if (url === "") {
      editor.chain().focus().unsetLink().run();
    } else {
      editor
        .chain()
        .focus()
        .extendMarkRange("link")
        .setLink({ href: url })
        .run();
    }
  };

  const triggerImagePicker = () => fileInputRef.current?.click();

  const handleImagePicked: React.ChangeEventHandler<HTMLInputElement> = async (e) => {
    const file = e.target.files?.[0];
    if (!file || !editor) return;

    // DEMO ONLY: show image immediately with a blob URL.
    // In production, upload to your server/CDN and use the returned URL.
    const objectUrl = URL.createObjectURL(file);
    editor.chain().focus().setImage({ src: objectUrl, alt: file.name }).run();
    // TODO: upload `file` to your backend and replace blob URL with permanent URL.
    e.target.value = "";
  };

  const handleSubmit: React.FormEventHandler<HTMLFormElement> = async (e) => {
    e.preventDefault();
    const contentHtml = editor?.getHTML() ?? "";
    const contentJson = editor?.getJSON() ?? null;

    // Example: send to your API
    // Replace /api/articles with your real endpoint
    // await fetch("/api/articles", {
    //   method: "POST",
    //   headers: { "Content-Type": "application/json" },
    //   body: JSON.stringify({ title, dir, contentHtml, contentJson }),
    // });

    console.log({ title, dir, contentHtml, contentJson });
    alert("Check the console for submitted payload!");
  };

  const ToolbarButton = (props: {
    onClick: () => void;
    active?: boolean;
    children: React.ReactNode;
    title?: string;
  }) => (
    <button
      type="button"
      className={props.active ? "is-active" : ""}
      onClick={props.onClick}
      title={props.title}
    >
      {props.children}
    </button>
  );

  // const canUndo = !!editor?.can().undo();
  // const canRedo = !!editor?.can().redo();

  return (
    <form onSubmit={handleSubmit} className="space-y-4">
      <div>
        <label style={{ display: "block", marginBottom: 6, fontWeight: 600 }}>
          Title
        </label>
        <input
          value={title}
          onChange={(e) => setTitle(e.target.value)}
          placeholder="Awesome article title"
          style={{
            width: "100%",
            padding: "10px 12px",
            borderRadius: 10,
            border: "1px solid #e5e7eb",
          }}
          required
        />
      </div>

      <div>
        <label style={{ display: "block", marginBottom: 6, fontWeight: 600 }}>
          Text Direction
        </label>
        <select value={dir} onChange={(e) => setDir(e.target.value as DirMode)}>
          <option value="auto">Auto (recommended)</option>
          <option value="ltr">Left-to-Right</option>
          <option value="rtl">Right-to-Left</option>
        </select>
      </div>

      <div className="editor-shell">
        <div className="toolbar">
          <ToolbarButton
            onClick={() => editor?.chain().focus().toggleBold().run()}
            active={editor?.isActive("bold")}
            title="Bold"
          >
            B
          </ToolbarButton>
          <ToolbarButton
            onClick={() => editor?.chain().focus().toggleItalic().run()}
            active={editor?.isActive("italic")}
            title="Italic"
          >
            I
          </ToolbarButton>
          <ToolbarButton
            onClick={() => editor?.chain().focus().toggleStrike().run()}
            active={editor?.isActive("strike")}
            title="Strike"
          >
            S
          </ToolbarButton>
          <ToolbarButton
            onClick={() => editor?.chain().focus().toggleCode().run()}
            active={editor?.isActive("code")}
            title="Inline code"
          >
            {"</>"}
          </ToolbarButton>

          <ToolbarButton
            onClick={() => editor?.chain().focus().toggleHeading({ level: 1 }).run()}
            active={editor?.isActive("heading", { level: 1 })}
            title="H1"
          >
            H1
          </ToolbarButton>
          <ToolbarButton
            onClick={() => editor?.chain().focus().toggleHeading({ level: 2 }).run()}
            active={editor?.isActive("heading", { level: 2 })}
            title="H2"
          >
            H2
          </ToolbarButton>
          <ToolbarButton
            onClick={() => editor?.chain().focus().toggleHeading({ level: 3 }).run()}
            active={editor?.isActive("heading", { level: 3 })}
            title="H3"
          >
            H3
          </ToolbarButton>

          <ToolbarButton
            onClick={() => editor?.chain().focus().toggleBulletList().run()}
            active={editor?.isActive("bulletList")}
            title="Bulleted list"
          >
            • List
          </ToolbarButton>
          <ToolbarButton
            onClick={() => editor?.chain().focus().toggleOrderedList().run()}
            active={editor?.isActive("orderedList")}
            title="Numbered list"
          >
            1. List
          </ToolbarButton>
          <ToolbarButton
            onClick={() => editor?.chain().focus().toggleBlockquote().run()}
            active={editor?.isActive("blockquote")}
            title="Quote"
          >
            ❝
          </ToolbarButton>

          <ToolbarButton
            onClick={() => editor?.chain().focus().setTextAlign("left").run()}
            active={editor?.isActive({ textAlign: "left" })}
            title="Align left"
          >
            ⬅
          </ToolbarButton>
          <ToolbarButton
            onClick={() => editor?.chain().focus().setTextAlign("center").run()}
            active={editor?.isActive({ textAlign: "center" })}
            title="Align center"
          >
            ⬌
          </ToolbarButton>
          <ToolbarButton
            onClick={() => editor?.chain().focus().setTextAlign("right").run()}
            active={editor?.isActive({ textAlign: "right" })}
            title="Align right"
          >
            ➡
          </ToolbarButton>

          <ToolbarButton onClick={setLink} title="Add/Remove link">
            🔗
          </ToolbarButton>

          <ToolbarButton onClick={triggerImagePicker} title="Insert image">
            🖼
          </ToolbarButton>
          <input
            ref={fileInputRef}
            type="file"
            accept="image/*"
            style={{ display: "none" }}
            onChange={handleImagePicked}
          />

          <ToolbarButton
            onClick={() => editor?.chain().focus().unsetAllMarks().run()}
            title="Clear marks"
          >
            Clear marks
          </ToolbarButton>
          <ToolbarButton
            onClick={() => editor?.chain().focus().clearNodes().run()}
            title="Clear nodes"
          >
            Clear blocks
          </ToolbarButton>

          <ToolbarButton
            onClick={() => editor?.chain().focus().undo().run()}
            title="Undo"
          >
            ⤺
          </ToolbarButton>
          <ToolbarButton
            onClick={() => editor?.chain().focus().redo().run()}
            title="Redo"
          >
            ⤻
          </ToolbarButton>
        </div>

        <EditorContent editor={editor} />
      </div>

      <div>
        <button
          type="submit"
          style={{
            padding: "10px 14px",
            borderRadius: 10,
            border: "1px solid #e5e7eb",
            background: "#111827",
            color: "white",
            fontWeight: 600,
          }}
        >
          Publish (demo)
        </button>
      </div>
    </form>
  );
}
