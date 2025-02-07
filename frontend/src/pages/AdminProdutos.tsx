/* eslint-disable @typescript-eslint/no-explicit-any */
import { useState, useEffect } from "react";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { v4 as uuidv4 } from 'uuid';

interface Product {
  id: string;
  name: string;
  price: number;
}

export const AdminProdutos = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [isModalOpen, setModalOpen] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState<any>(null);
  const [productName, setProductName] = useState("");
  const [productPrice, setProductPrice] = useState(0);

  useEffect(() => {
    const fetchOrders = async () => {
      try {
        const response = await fetch("http://localhost:5235/api/Products/products");
        if (!response.ok) {
          throw new Error("Erro ao buscar produtos");
        }
        const data = await response.json();
        console.log(JSON.stringify(data));
        setProducts(data);
      } catch (error) {
        console.error("Erro ao buscar produtos:", error);
      }
    };

    fetchOrders();
  }, []);

  const openModal = (product?: Product) => {
    if (product) {
      setSelectedProduct(product);
      setProductName(product.name);
      setProductPrice(product.price);
    } else {
      setSelectedProduct(null);
      setProductName("");
      setProductPrice(0);
    }
    setModalOpen(true);
  };

  const closeModal = () => {
    setSelectedProduct(null);
    setProductName("");
    setProductPrice(0);
    setModalOpen(false);
  };

  const saveProduct = () => {
    if (!productName || !productPrice) {
      toast.error("Preencha todos os campos!", { autoClose: 2000 });
      return;
    }

    if (selectedProduct) {     
      setProducts((prev) =>
        prev.map((p) =>
          p.id === selectedProduct.id ? { ...p, name: productName, price: Number(productPrice) } : p
        )
      );
      toast.success("Produto atualizado com sucesso!", { autoClose: 2000 });

      
      fetch(`http://localhost:5235/api/Products/products/${selectedProduct.id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name: productName, price: Number(productPrice) }),
      })
      .then(response => response.json())
      .then(data => console.log("Produto atualizado:", data))
      .catch(error => console.error("Erro ao atualizar produto:", error));
      
    } else {
      const newProduct = { id: uuidv4(), name: productName, price: Number(productPrice) };
      setProducts((prev) => [...prev, newProduct]);
      toast.success("Produto adicionado com sucesso!", { autoClose: 2000 });

      
      fetch("http://localhost:5235/api/Products/products", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newProduct),
      })
      .then(response => response.json())
      .then(data => console.log("Novo produto adicionado:", data))
      .catch(error => console.error("Erro ao adicionar produto:", error));
    }

    closeModal();
  };

  const deleteProduct = (id: string) => {
    setProducts((prev) => prev.filter((p) => p.id !== id));
    toast.success("Produto excluído com sucesso!", { autoClose: 2000 });

    
    fetch(`http://localhost:5235/api/Products/products/${id}`, {
      method: "DELETE",
    })
    .then(response => {
      if (!response.ok) throw new Error("Falha ao excluir produto");
      console.log("Produto excluído com sucesso");
    })
    .catch(error => console.error("Erro ao excluir produto:", error));
    
    
  };

  return (
    <div style={{ minHeight: "90vh", backgroundColor: "#f4f1ea", padding: "20px" }}>
      <ToastContainer />
      <div
        style={{
          maxWidth: "1200px",
          margin: "0 auto",
          backgroundColor: "#fff",
          boxShadow: "0px 4px 6px rgba(0, 0, 0, 0.1)",
          padding: "20px",
          borderRadius: "8px",
          fontFamily: "'Inter', sans-serif",
        }}
      >
        <h2 style={{ fontSize: "24px", fontWeight: "bold", textAlign: "center" }}>
          Gerenciar Produtos
        </h2>

        <button
          onClick={() => openModal()}
          style={{
            display: "block",
            margin: "20px auto",
            padding: "10px 20px",
            backgroundColor: "#4CAF50",
            color: "#fff",
            border: "none",
            borderRadius: "8px",
            fontWeight: "bold",
            cursor: "pointer",
          }}
        >
          Adicionar Produto
        </button>

        <div style={{ overflowX: "auto", marginTop: "20px" }}>
          <table style={{ width: "100%", textAlign: "left", borderCollapse: "collapse", fontSize: "16px" }}>
            <thead>
              <tr>
                <th style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>ID</th>
                <th style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>Nome</th>
                <th style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>Preço</th>
                <th style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>Ações</th>
              </tr>
            </thead>
            <tbody>
              {products.map((product) => (
                <tr key={product.id}>
                  <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>{product.id}</td>
                  <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>{product.name}</td>
                  <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>R$ {product.price.toFixed(2)}</td>
                  <td style={{ padding: "10px", borderBottom: "1px solid #ddd", display: "flex", gap: "8px" }}>
                    <button style={{
                      color: "#ff1744",
                      backgroundColor: "#fce4ec",
                      border: "none",
                      borderRadius: "8px",
                      padding: "10px",
                      cursor: "pointer",
                      fontWeight: "bold",
                    }}
                      onClick={() => openModal(product)}>Editar</button>
                    <button style={{
                      color: "#ff1744",
                      backgroundColor: "#fce4ec",
                      border: "none",
                      borderRadius: "8px",
                      padding: "10px",
                      cursor: "pointer",
                      fontWeight: "bold",
                    }}
                      onClick={() => deleteProduct(product.id)}>Excluir</button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>

      {isModalOpen && (
        <div style={{
          position: "fixed",
          fontFamily: "'Inter', sans-serif",
          top: 0, left: 0, width: "100%", height: "100%",
          backgroundColor: "rgba(0, 0, 0, 0.5)",
          display: "flex", justifyContent: "center", alignItems: "center"
        }}>
          <div style={{
            backgroundColor: "#fff",
            padding: "20px",
            borderRadius: "8px",
            maxWidth: "400px",
            minWidth: "300px",
            width: "100%",
            boxShadow: "0px 4px 6px rgba(0, 0, 0, 0.2)"
          }}>
            <h3 style={{ marginBottom: "10px", textAlign: "center" }}>
              {selectedProduct ? "Editar Produto" : "Adicionar Produto"}
            </h3>

            <input
              type="text"
              placeholder="Nome do Produto"
              value={productName}
              onChange={(e) => setProductName(e.target.value)}
              style={{
                width: "100%",
                padding: "10px",
                marginBottom: "10px",
                borderRadius: "8px",
                border: "1px solid #ddd",
                fontSize: "16px",
                boxSizing: "border-box",
              }}
            />

            <input
              type="text"
              placeholder="Preço (R$ 0,00)"
              value={productPrice === 0 ? "" : productPrice}
              onChange={(e) => {
                let value = e.target.value.replace(/[^0-9.,]/g, "");
                value = value.replace(",", ".");
                const numericValue = !isNaN(parseFloat(value)) ? parseFloat(value) : productPrice;
                setProductPrice(numericValue);
              }}
              style={{
                width: "100%",
                padding: "10px",
                marginBottom: "10px",
                borderRadius: "8px",
                border: "1px solid #ddd",
                fontSize: "16px",
                boxSizing: "border-box",
              }}
            />

            <div style={{ display: "flex", gap: "10px", marginTop: "10px" }}>
              <button style={{
                color: "#fff",
                backgroundColor: "#4CAF50",
                border: "none",
                borderRadius: "8px",
                padding: "10px",
                cursor: "pointer",
                fontWeight: "bold",
                flex: 1
              }} onClick={saveProduct}>Salvar</button>

              <button style={{
                backgroundColor: "#F44336",
                color: "#fff",
                border: "none",
                borderRadius: "8px",
                padding: "10px",
                cursor: "pointer",
                fontWeight: "bold",
                flex: 1
              }} onClick={closeModal}>Cancelar</button>
            </div>
          </div>
        </div>
      )}


    </div>
  );
};

export default AdminProdutos;
