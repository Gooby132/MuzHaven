import React, { useState } from "react";
import {
  Navigate,
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import { Login } from "../pages/Login";
import { Register } from "../pages/Register";
import { Error } from "../pages/Error";
import { Profile } from "../pages/Profile";
import { useSelector } from "react-redux";
import { RootState } from "../../redux/store";
import Projects from "../pages/Projects";
// @ts-ignore
import Modal from "react-modal";
import { MainVer2 } from "components/layout/app/MainVer2";
import { Project } from "components/pages/Project";
import { fetchProjectById } from "services/project/projectServiceClient";

function App() {
  const user = useSelector((state: RootState) => state.user);

  const router = createBrowserRouter([
    {
      path: "/",
      element: <MainVer2 />,
      children: [
        {
          path: "/login",
          errorElement: <Error />,
          element: <Login />,
        },
        {
          path: "/register",
          errorElement: <Error />,
          element: user.loggedIn ? (
            <Navigate to="/register" replace />
          ) : (
            <Register />
          ),
        },
        {
          path: "/profile",
          errorElement: <Error />,
          element: !user.loggedIn ? (
            <Navigate to="/login" replace />
          ) : (
            <Profile />
          ),
        },
        {
          path: "/projects",
          errorElement: <Error />,
          element: !user.loggedIn ? (
            <Navigate to="/projects" replace />
          ) : (
            <Projects />
          ),
        },
        {
          path: "/project/:id",
          element: !user.loggedIn ? (
            <Navigate to="/login" replace />
          ) : (
            <Project />
          ),
          loader: async ({ params }) => {
            if (params.id === undefined) throw new Response("No id give");
            const res = await fetchProjectById({
              id: params.id,
              token: user.token!,
            });

            if (res.isError) throw new Response("failure");

            return res.result;
          },
        },
      ],
    },
  ]);

  return <RouterProvider router={router} fallbackElement={<Error />} />;
}

Modal.setAppElement("#root");

export default App;
