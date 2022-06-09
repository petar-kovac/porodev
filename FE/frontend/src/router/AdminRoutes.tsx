import { FC, ReactNode } from 'react';
import { Navigate, Outlet } from 'react-router-dom';
import { useAuthStateValue } from '../context/AuthContext';

const AdminRoutes: FC<{ children?: ReactNode; isAdmin: boolean }> = ({
  children,
  isAdmin,
}) => {
  return isAdmin ? <Outlet /> : <Navigate to="/notallowed" />;
};

export default AdminRoutes;
