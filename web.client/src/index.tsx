import React from "react";
import ReactDOM from "react-dom/client";
import "./index.css";
import App from "components/app/App";
import reportWebVitals from "./reportWebVitals";
import { persistor, store } from "./redux/store";
import { Provider } from "react-redux";
import { KEY_PREFIX } from "redux-persist";
import { PersistGate } from "redux-persist/integration/react";

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);

root.render(
  <React.StrictMode>
    <PersistGate persistor={persistor}>
        <Provider store={store}>
          <App />
        </Provider>
    </PersistGate>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
