import { FC } from 'react';
import { Navigate, Outlet } from 'react-router-dom';

const AdminRoutes: FC<{
  isAdmin: boolean;
  location: any;
}> = ({ isAdmin, location }) => {
  return isAdmin ? (
    <Outlet />
  ) : location.pathname === '/' ? (
    <Navigate to="/user-home" />
  ) : (
    <Navigate to="/notallowed" />
  );
};

export default AdminRoutes;
