import { BrowserRouter } from "react-router-dom";
import { AppRoutes } from "./routes";
import { Navbar } from "./components/Navbar";
import { CartProvider } from "./context/CartContext";

const App = () => {
  return (
    <CartProvider>
      <BrowserRouter>
        <Navbar />
        <AppRoutes />
      </BrowserRouter>
    </CartProvider>
  );
};

export default App;
