  /* eslint-disable @typescript-eslint/no-explicit-any */
  import { useState, useEffect } from "react";
  import { ToastContainer, toast } from "react-toastify";
  import "react-toastify/dist/ReactToastify.css";

  interface Order {
    id: number;
    orderId: string;
    orderDate: string;
    totalAmount: number;
    status: number;
    orderItems: [
      {
        id: number;
        productName: string;
        quantity: number;
        unitPrice: number;
        totalPrice: number;
      }
    ];
  }

  export const AdminPedidos = () => {
    const [orders, setOrders] = useState<Order[]>([]);
    const [selectedOrder, setSelectedOrder] = useState<Order | null>(null);
    const [isModalOpen, setModalOpen] = useState(false);
    const [isStatusModalOpen, setStatusModalOpen] = useState(false);
    const [newStatus, setNewStatus] = useState<string | null>(null);

    useEffect(() => {
      const fetchOrders = async () => {
        try {
          const response = await fetch("http://localhost:5000/api/OrderRead");
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
      const statusMap: { [key: string]: { label: string; color: string; enumValue: Number } } = {
        'Pending' : { label: "Pendente", color: "#FFA726", enumValue: 0 },
        'Paid': { label: "Pago", color: "#4CAF50", enumValue: 1 },
        'Shipped': { label: "Enviado", color: "#29B6F6", enumValue: 2 },
        'Delivered': { label: "Entregue", color: "#8BC34A", enumValue: 3 },
        'Canceled': { label: "Cancelado", color: "#F44336", enumValue: 4 }
      };
      return statusMap[status] || { label: "Desconhecido", color: "#BDBDBD", num: 0 };
    };

    const getStatusEnumInfo = (status: number) => {
      const statusMap: { [key: number]: { label: string } } = {
        0 : { label: "Pendente" },
        1 : { label: "Pago" },
        2 : { label: "Enviado" },
        3 : { label: "Entregue" },
        4 : { label: "Cancelado" }
      };
      return statusMap[status] || { label: "Desconhecido" };
    };

    const getTranslatedStatus = (status: string) => {
      const statusMap: { [key: string]: { label: string } } = {
        'Pendente' : { label: "Pending" },
        'Pago': { label: "Paid" },
        'Enviado': { label: "Shipped" },
        'Entregue': { label: "Delivered" },
        'Cancelado': { label: "Canceled" }
      };
      return statusMap[status] || { label: "Pending" };
    };

    const deleteOrder = (orderId: string) => {
      toast.success("Pedido excluído com sucesso!", { autoClose: 2000 });

      setOrders((prevOrders) => prevOrders.filter((order) => order.orderId !== orderId));
      closeModal();

      fetch(`http://localhost:5000/api/Orders/${orderId}`, {
        method: "DELETE",
      })
        .then(response => response.json())
        .then(data => {
          console.log("Pedido excluído:", data);
          setOrders(prevOrders => prevOrders.filter(order => order.orderId !== orderId));
        })
        .catch(error => console.error("Erro ao excluir pedido:", error));
    };

    const openModal = (order: Order) => {
      setSelectedOrder(order);
      setModalOpen(true);
    };

    const closeModal = () => {
      setSelectedOrder(null);
      setModalOpen(false);
    };

    const openStatusModal = (order: Order) => {
      setSelectedOrder(order);
      const statusNum = getStatusEnumInfo(order.status).label;
      setNewStatus(statusNum);
      setStatusModalOpen(true);
    };
    
    
    const closeStatusModal = () => {
      setSelectedOrder(null);
      setNewStatus(null);
      setStatusModalOpen(false);
    };

    const updateOrderStatus = () => {
      if (selectedOrder && newStatus !== null) {
        toast.success("Status do pedido atualizado!", { autoClose: 2000 });
        
        const status = getTranslatedStatus(newStatus).label;
        const statusEnum = getStatusInfo(status);
        setNewStatus(newStatus);

        
        setStatusModalOpen(false);

        fetch(`http://localhost:5000/api/Orders/${selectedOrder.orderId}/status`, {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({ status: statusEnum.enumValue }), 
        })
          .then(response => response.json())
          .then(data => {
            console.log("Pedido atualizado:", data);
           // fetchOrders();  
          })
          .catch(error => console.error("Erro ao atualizar pedido:", error));
      }
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
            Gerenciar Pedidos
          </h2>

          <div style={{ overflowX: "auto", marginTop: "20px" }}>
            <table style={{
              width: "100%",
              borderCollapse: "collapse",
              textAlign: "left",
              fontSize: "16px",
            }}>
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
                    <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>{order.orderId}</td>
                    <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>{new Date(order.orderDate).toLocaleDateString()}</td>
                    <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>R$ {order.totalAmount.toFixed(2)}</td>
                    <td style={{ color: getStatusInfo(order.status.toString()).color, fontWeight: "bold", padding: "10px", borderBottom: "1px solid #ddd" }}>
                      {getStatusInfo(order.status.toString()).label}
                    </td>
                    <td style={{ padding: "10px", borderBottom: "1px solid #ddd", display: "flex", gap: "8px" }}>
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
                        onClick={() => openStatusModal(order)}
                      >
                        Alterar Status
                      </button>
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
                    <th style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>Preço Unitário</th>
                    <th style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>Total</th>
                  </tr>
                </thead>
                <tbody>
                  {selectedOrder.orderItems.map((item) => (
                    <tr key={item.id}>
                      <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>{item.productName}</td>
                      <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>{item.quantity}</td>
                      <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>R$ {item.unitPrice.toFixed(2)}</td>
                      <td style={{ padding: "10px", borderBottom: "1px solid #ddd" }}>R$ {item.totalPrice.toFixed(2)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
              <div
                style={{
                  display: "flex",
                  justifyContent: "space-between",
                  marginTop: "20px",
                }}
              >
                <button
                  style={{
                    backgroundColor: "#f44336",
                    color: "#fff",
                    padding: "10px 20px",
                    border: "none",
                    borderRadius: "8px",
                    cursor: "pointer",
                    fontWeight: "bold",
                  }}
                  onClick={() => deleteOrder(selectedOrder.orderId)}
                >
                  Excluir Pedido
                </button>
                <button
                  style={{
                    backgroundColor: "#29b6f6",
                    color: "#fff",
                    padding: "10px 20px",
                    border: "none",
                    borderRadius: "8px",
                    cursor: "pointer",
                    fontWeight: "bold",
                  }}
                  onClick={closeModal}
                >
                  Fechar
                </button>
              </div>
            </div>
          </div>
        )}

        {isStatusModalOpen && selectedOrder && (
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
              <h3 style={{ marginBottom: "20px" }}>Alterar Status</h3>
              <select
                value={newStatus !== null ? newStatus : 0}
                onChange={(e) => setNewStatus(e.target.value)}
                style={{
                  width: "100%",
                  padding: "10px",
                  marginBottom: "10px",
                  borderRadius: "8px",
                  border: "1px solid #ddd",
                  backgroundColor: "#fff",
                  fontSize: "16px",
                  cursor: "pointer",
                }}
              >
                {['Pending', 'Paid', 'Shipped', 'Delivered', 'Canceled'].map((status) => (
                  <option key={status} value={getStatusInfo(status).label}>
                    {getStatusInfo(status).label}
                  </option>
                ))}
              </select>
              <div
                style={{
                  display: "flex",
                  justifyContent: "space-between",
                  marginTop: "20px",
                }}
              >
                <button
                  style={{
                    backgroundColor: "#29b6f6",
                    color: "#fff",
                    padding: "10px 20px",
                    border: "none",
                    borderRadius: "8px",
                    cursor: "pointer",
                    fontWeight: "bold",
                  }}
                  onClick={updateOrderStatus}
                >
                  Salvar Alterações
                </button>
                <button
                  style={{
                    backgroundColor: "#f44336",
                    color: "#fff",
                    padding: "10px 20px",
                    border: "none",
                    borderRadius: "8px",
                    cursor: "pointer",
                    fontWeight: "bold",
                  }}
                  onClick={closeStatusModal}
                >
                  Fechar
                </button>
              </div>
            </div>
          </div>
        )}
      </div>
    );
  };


  export default AdminPedidos;
