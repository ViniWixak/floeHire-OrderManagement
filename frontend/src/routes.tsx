import { Routes, Route } from "react-router-dom";
import { Home } from "./pages/Home";
import { Pedidos } from "./pages/Pedidos";
import AdminPedidos from "./pages/AdminPedidos";
import AdminProdutos from "./pages/AdminProdutos";

export const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/pedidos" element={<Pedidos />} />
      <Route path="/admin-pedidos" element={<AdminPedidos />} />
      <Route path="/adminProdutos" element={<AdminProdutos />} />
    </Routes>
  );
};
