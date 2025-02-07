import { useEffect, useState } from "react";
import { useCart } from "../context/CartContext"; 
import pizzaImage from "../assets/pizza.png";
import { FiTrash2 } from "react-icons/fi";
import { toast, ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css"; 

interface Product {
  id: number;
  name: string;
  price: number;
}

export const Home = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const { cart, removeFromCart, clearCart } = useCart(); // Usa o carrinho do contexto


  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const response = await fetch("http://localhost:5235/api/Products/products");
        if (!response.ok) {
          throw new Error("Erro ao buscar produtos");
        }
        console.log(response);
        const data = await response.json();
        setProducts(data);
      } catch (error) {
        console.error("Erro ao buscar produtos:", error);
      }
      
    };
    fetchProducts();
  }, []);

  const handleFinalizeOrder = () => {
    const orderData = cart.map((item) => ({
      id: item.id,
      name: item.name,
      quantity: item.quantity,
      totalPrice: item.price * item.quantity,
    }));

    const totalOrderPrice = orderData.reduce((acc, item) => acc + item.totalPrice, 0);


    const sendData = {
      name: 'name usuario',
      id: 'idUsuario',
      totalAmount: totalOrderPrice,
      products: orderData,
    }

    console.log("Pedido enviado:", orderData);
    toast.success("Pedido finalizado com sucesso! 🎉", {
      position: "top-right",
      autoClose: 3000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
    });
    clearCart()

    fetch("http://localhost:5235/api/Orders", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(sendData),
    })
      .then((response) => response.json())
      .then((data) => {
        console.log("Resposta do servidor:", data);
    toast.success("Pedido finalizado com sucesso! 🎉", {
      position: "top-right",
      autoClose: 3000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
    });
      })
      .catch((error) => {
        console.error("Erro ao enviar pedido:", error);
      toast.error("Erro ao finalizar o pedido. Tente novamente.", {
      position: "top-right",
      autoClose: 3000,
      hideProgressBar: false,
      closeOnClick: true,
      pauseOnHover: true,
      draggable: true,
      progress: undefined,
    });
      });
  };


  return (
    <div style={{
      minHeight: "90vh",
      backgroundColor: "#f4f1ea",
    }}>
      <ToastContainer />
      <div
        style={{
          display: "flex",
          marginLeft: "auto",
          marginRight: "auto",
          width: "100%",
          maxWidth: "1200px",
          paddingTop: "30px"
        }}
      >
        <div
          style={{
            flex: 2,
            display: "grid",
            gridTemplateColumns: "repeat(3, 1fr)",
            gap: "30px",
            padding: "20px",
            margin: "0 auto",
          }}
        >
          {products.map((product) => (
            <ProductCard key={product.id} product={product} />
          ))}
        </div>
        <div
          style={{
            flex: 0.8,
            display: "flex",
            flexDirection: "column",
            gap: "20px",
            padding: "20px",
            maxWidth: "1280px",
            margin: "0 auto",
            fontFamily: "'Inter', sans-serif",
          }}
        >
          <div
            style={{
              backgroundColor: "#fff",
              borderLeft: "1px solid #ddd",
              boxShadow: "0px 4px 6px rgba(0, 0, 0, 0.1)",
              height: "400px",
              display: "flex",
              flexDirection: "column",
            }}
          >
            <h2
              style={{
                fontSize: "24px",
                fontWeight: "bold",
                padding: "20px",
                borderBottom: "1px solid #eee",
                textAlign: "center",
              }}
            >
              Carrinho
            </h2>
            <div
              style={{
                flex: 1,
                overflowY: "auto",
                padding: "20px",
              }}
            >
              {cart.length > 0 ? (
                cart.map((item) => (
                  <div
                    key={item.id}
                    style={{
                      display: "flex",
                      justifyContent: "space-between",
                      alignItems: "center",
                      marginBottom: "15px",
                      paddingBottom: "15px",
                      borderBottom: "1px solid #eee",
                      flexWrap: "wrap",
                    }}
                  >
                    <div>
                      <h4 style={{ fontSize: "16px", fontWeight: "bold", margin: 0 }}>
                        {item.name}
                      </h4>
                      <p
                        style={{
                          fontSize: "14px",
                          margin: "5px 0",
                          color: "#757575",
                        }}
                      >
                        Quantidade: {item.quantity}
                      </p>
                      <FiTrash2
                        style={{
                          cursor: "pointer",
                          color: "#ff1744",
                          marginTop: "5px",
                        }}
                        size={18}
                        onClick={() => removeFromCart(item.id)} 
                      />
                    </div>
                    <p style={{ fontWeight: "bold" }}>
                      R${(item.price * item.quantity).toFixed(2)}
                    </p>
                  </div>
                ))
              ) : (
                <p
                  style={{
                    textAlign: "center",
                    fontSize: "16px",
                    color: "#757575",
                  }}
                >
                  Seu carrinho está vazio.
                </p>
              )}
            </div>

            {cart.length > 0 && (
              <div
                style={{
                  padding: "20px",
                  borderTop: "1px solid #eee",
                }}
              >
                <div
                  style={{
                    display: "flex",
                    justifyContent: "space-between",
                    alignItems: "center",
                    marginBottom: "15px",
                    fontSize: "18px",
                    fontWeight: "bold",
                  }}
                >
                  <span>Total:</span>
                  <span>
                    R$
                    {cart
                      .reduce((total, item) => total + item.price * item.quantity, 0)
                      .toFixed(2)}
                  </span>
                </div>

                <button
                  style={{
                    width: "100%",
                    padding: "10px",
                    color: "#ff1744",
                    backgroundColor: "#fce4ec",
                    border: "none",
                    borderRadius: "8px",
                    fontWeight: "bold",
                    cursor: "pointer",
                  }}
                  onClick={handleFinalizeOrder}
                >
                  Finalizar Pedido
                </button>
              </div>
            )}
          </div>
        </div>


      </div>
    </div>

  );
};

const ProductCard: React.FC<{ product: Product }> = ({ product }) => {
  const [quantity, setQuantity] = useState(1);
  const { addToCart } = useCart();

  const handleIncrement = () => setQuantity((prev) => prev + 1);
  const handleDecrement = () => setQuantity((prev) => (prev > 1 ? prev - 1 : 1));
  const handleAddToCart = () => {
    addToCart({ ...product, quantity });
    setQuantity(1);
  };

  return (
    <div
      style={{
        border: "1px solid #ddd",
        borderRadius: "8px",
        textAlign: "center",
        height: "250px",
        backgroundColor: "#fff",
        fontFamily: "'Inter', sans-serif",
        flexDirection: "column",
        padding: "20px",
        boxShadow: "0px 4px 6px rgba(0, 0, 0, 0.1)",
      }}
    >
      <img
        src={pizzaImage}
        alt={product.name}
        style={{ width: "100px", marginTop: "-50px", height: "100px" }}
      />
      <h3>{product.name}</h3>
      <p style={{ fontWeight: "bold" }}>R${product.price.toFixed(2)}</p>

      <div
        style={{
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          gap: "10px",
          margin: "10px 0",
        }}
      >
        <button
          onClick={handleDecrement}
          style={{
            backgroundColor: "#fce4ec",
            border: "none",
            borderRadius: "50%",
            width: "32px",
            height: "32px",
            fontSize: "18px",
            fontWeight: "bold",
            color: "#ff1744",
            cursor: "pointer",
          }}
        >
          -
        </button>
        <span style={{ fontSize: "18px", fontWeight: "bold" }}>{quantity}</span>
        <button
          onClick={handleIncrement}
          style={{
            backgroundColor: "#fce4ec",
            border: "none",
            borderRadius: "50%",
            width: "32px",
            height: "32px",
            fontSize: "18px",
            fontWeight: "bold",
            color: "#ff1744",
            cursor: "pointer",
          }}
        >
          +
        </button>
      </div>
      <button
        onClick={handleAddToCart}
        style={{
          color: "#ff1744",
          backgroundColor: "#fce4ec",
          border: "none",
          padding: "10px 20px",
          borderRadius: "8px",
          marginTop: "10px",
          cursor: "pointer",
        }}
      >
        Adicionar ao Carrinho
      </button>
    </div>
  );
};

export default ProductCard;
