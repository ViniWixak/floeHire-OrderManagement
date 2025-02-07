import { Link } from "react-router-dom";

export const Navbar = () => {
  return (
    <nav
      style={{
        display: "flex",
        alignItems: "center",
        justifyContent: "space-between",
        padding: "10px 20px",
        backgroundColor: "#fff",
        borderBottom: "1px solid #ddd",
        boxShadow: "0 2px 4px rgba(0, 0, 0, 0.1)",
      }}
    >
      <div style={{ display: "flex", marginLeft: "auto", marginRight: "auto", gap: "40px", padding: "10px", }}>
        <Link
          to="/"
          style={{
            textDecoration: "none",
            color: "#333",
            fontWeight: "500",
            fontSize: "1.2rem",
            fontFamily: "'Inter', sans-serif"
          }}
        >
          Home
        </Link>
        <Link
          to="/pedidos"
          style={{
            textDecoration: "none",
            color: "#333",
            fontWeight: "500",
            fontSize: "1.2rem",
            fontFamily: "'Inter', sans-serif"
          }}
        >
          Pedidos
        </Link>
        <Link
          to="/admin-pedidos"
          style={{
            textDecoration: "none",
            color: "#333",
            fontWeight: "500",
            fontSize: "1.2rem",
            fontFamily: "'Inter', sans-serif"
          }}
        >
          Admin Pedidos
        </Link>
        <Link
          to="/adminProdutos"
          style={{
            textDecoration: "none",
            color: "#333",
            fontWeight: "500",
            fontSize: "1.2rem",
            fontFamily: "'Inter', sans-serif"
          }}
        >
          Admin Produtos
        </Link>
      </div>
    </nav>
  );
};
