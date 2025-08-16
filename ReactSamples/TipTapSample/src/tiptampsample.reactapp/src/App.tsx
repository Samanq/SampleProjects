import "./App.css";
import ArticleEditor from "./components/ArticleEditor";

function App() {
  return (
    <>
       <div style={{ maxWidth: 820, margin: "40px auto", padding: "0 16px" }}>
        <h1 style={{ fontSize: 28, marginBottom: 16 }}>Write a new article</h1>
        <ArticleEditor />
      </div>
    </>
  );
}

export default App;
