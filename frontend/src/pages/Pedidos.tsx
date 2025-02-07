/* eslint-disable @typescript-eslint/no-explicit-any */
import { useState, useEffect } from "react";

interface Order {
  id: number;
  orderId: string;
  orderDate: string;
  totalAmount: number;
  status: string;
  orderItems: [
    {
      id: number;
      productName: string;
      quantity: number;
      unitPrice: number;
      totalPrice: number;
    }];
}

export const Pedidos = () => {
  const [orders, setOrders] = useState<Order[]>([]);
  const [selectedOrder, setSelectedOrder] = useState<any>(null); 
  const [isModalOpen, setModalOpen] = useState(false);

  useEffect(() => {
    const fetchOrders = async () => {
      try {
        const response = await fetch("http://localhost:5235/api/OrderRead");
        if (!response.ok) {
          throw new Error("Erro ao buscar pedidos");
        }
        const data = await response.json();
        console.log(JSON.stringify(data));
        setOrders(data);
      } catch (error) {
        console.error("Erro ao buscar pedidos:", error);
      }
    };

    fetchOrders();
  }, []);

  const getStatusInfo = (status: string) => {
    const statusMap: { [key: string]: { label: string; color: string } } = {
      'Pending': { label: "Pendente", color: "#FFA726" }, 
      'Paid': { label: "Pago", color: "#4CAF50" }, 
      'Shipped': { label: "Enviado", color: "#29B6F6" },
      'Delivered': { label: "Entregue", color: "#8BC34A" }, 
      'Canceled': { label: "Cancelado", color: "#F44336" }
    };
    return statusMap[status] || { label: "Desconhecido", color: "#BDBDBD" }; 
  };

  const openModal = (order: any) => {
    setSelectedOrder(order);
    setModalOpen(true);
  };

  const closeModal = () => {
    setSelectedOrder(null);
    setModalOpen(false);
  };

  return (
    <div style={{ minHeight: "90vh", backgroundColor: "#f4f1ea", padding: "20px" }}>
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
          Meus Pedidos
        </h2>

        <div style={{ overflowX: "auto", marginTop: "20px" }}>
          <table
            style={{
              width: "100%",
              borderCollapse: "collapse",
              textAlign: "left",
              fontSize: "16px",
            }}
          >
            <thead>
              <tr>
                <th style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>Pedido ID</th>
                <th style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>Data</th>
                <th style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>Total</th>
                <th style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>Status</th>
                <th style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>Ações</th>
              </tr>
            </thead>
            <tbody>
              {orders.map((order) => (
                <tr key={order.id}>
                  <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>
                    {order.orderId}
                  </td>
                  <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>
                    {new Date(order.orderDate).toLocaleDateString()}
                  </td>
                  <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>
                    R$ {order.totalAmount.toFixed(2)}
                  </td>
                  <td
                    style={{
                      padding: "10px",
                      borderBottom: "1px solid #ddd",
                      color: getStatusInfo(order.status).color,
                      fontWeight: "bold",
                    }}
                  >
                    {getStatusInfo(order.status).label}
                  </td>
                  <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>
                    <button
                      style={{
                        color: "#ff1744",
                        backgroundColor: "#fce4ec",
                        border: "none",
                        borderRadius: "8px",
                        padding: "10px",
                        cursor: "pointer",
                        fontWeight: "bold",
                      }}
                      onClick={() => openModal(order)}
                    >
                      Detalhes
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>

      {isModalOpen && selectedOrder && (
        <div
          style={{
            position: "fixed",
            top: 0,
            left: 0,
            width: "100%",
            height: "100%",
            backgroundColor: "rgba(0, 0, 0, 0.5)",
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
          }}
        >
          <div
            style={{
              backgroundColor: "#fff",
              padding: "20px",
              borderRadius: "8px",
              maxWidth: "600px",
              width: "100%",
              boxShadow: "0px 4px 6px rgba(0, 0, 0, 0.2)",
              fontFamily: "'Inter', sans-serif",
            }}
          >
            <h3 style={{ marginBottom: "20px" }}>Detalhes do Pedido</h3>
            <table style={{ width: "100%", borderCollapse: "collapse" }}>
              <thead>
                <tr>
                  <th style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>Produto</th>
                  <th style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>Quantidade</th>
                  <th style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>Preço</th>
                  <th style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>Total</th>
                </tr>
              </thead>
              <tbody>
                {selectedOrder.orderItems.map((item: any) => (
                  <tr key={item.id}>
                    <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>
                      {item.productName}
                    </td>
                    <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>
                      {item.quantity}
                    </td>
                    <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>
                      R$ {item.unitPrice.toFixed(2)}
                    </td>
                    <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>
                      R$ {item.totalPrice.toFixed(2)}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
            <button
              style={{
                marginTop: "20px",
                backgroundColor: "#F44336",
                color: "#fff",
                border: "none",
                borderRadius: "8px",
                padding: "10px",
                cursor: "pointer",
                fontWeight: "bold",
              }}
              onClick={closeModal}
            >
              Fechar
            </button>
          </div>
        </div>
      )}
    </div>
  );
};

export default Pedidos;
