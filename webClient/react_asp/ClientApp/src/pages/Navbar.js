import { Link } from 'react-router-dom'
const Navbar = () => {
  return (

    <div>
      <ul>
        <li><Link to="/">My project</Link></li>
        <li><Link to="/login">Login</Link></li>
        <li> <Link to="/register">Register</Link></li>
        <li> <Link to="/signout">Signout</Link></li>
        <li> <Link to="/fileupload">FileUpload</Link>
        </li>
      </ul>
    </div>
  );
};

export default Navbar;
