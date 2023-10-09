import { Layout } from "./pages/Layout";
import { Home } from "./pages/Home";
import { Register } from "./pages/Register";
import { Login } from "./pages/Login";
import { FetchData } from "./pages/FetchData";
import { FileUpload } from "./pages/FileUpload";
import { store } from './store/configureStore';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Provider } from 'react-redux';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <Provider store={store}>
    <BrowserRouter>
      <Layout>
        <Routes>
          <Route path="/" element={<Home />} /> 
          <Route path="/register" element={<Register />} />
          <Route path="/Login" element={<Login />} />
          <Route path="/fileupload" element={<FileUpload />} />
          <Route path="/Fetch-data" element={<FetchData />} />
        </Routes>
      </Layout>
    </BrowserRouter>
  </Provider>,
);


reportWebVitals();
