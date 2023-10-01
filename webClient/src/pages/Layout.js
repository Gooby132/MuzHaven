
import Navbar from './Navbar';
import { Outlet, Link } from "react-router-dom";
// import './tailwind.css';
const Layout = () => {
  return (
    <div className="App">
      <Navbar />
      <br /><br />
      <h1>Layout</h1>
      <Outlet />
    </div>
  );
}

export default Layout;
