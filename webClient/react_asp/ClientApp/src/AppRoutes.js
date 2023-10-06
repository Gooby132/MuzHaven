import {
  FetchData,
  Login,
  Register,
  Contact,
  FileUpload,
  Home,
} from "./components";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/contact',
    element: <Contact />
  },
  {
    path: '/login',
    element: <Login />
  },
  {
    path: '/register',
    element: <Register />
  },
  {
    path: '/fileupload',
    element: <FileUpload />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
];

export default AppRoutes;
