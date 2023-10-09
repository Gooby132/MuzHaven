// Layout.js
import React from 'react';
import { NavMenu } from './NavMenu';

export const Layout = ({ children }) => {
  return (
    <div>
      <NavMenu />
      {children}
    </div>
  );
};

export default Layout;
