import { FC, ReactNode } from 'react';
import { Navigate, Outlet } from 'react-router-dom';

const AdminRoutes: FC<{
  children?: ReactNode;
  isAdmin: boolean;
  location: any;
}> = ({ children, isAdmin, location }) => {
  return isAdmin ? (
    <Outlet />
  ) : location.pathname === '/' ? (
    <Navigate to="/user-home" />
  ) : (
    <Navigate to="/notallowed" />
  );
};

export default AdminRoutes;
