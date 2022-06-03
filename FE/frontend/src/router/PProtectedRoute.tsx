import { FC, ReactNode } from 'react';
import { Navigate } from 'react-router-dom';
import { useAuthStateValue } from '../context/AuthContext';

const PProtectedRoute: FC<{ children?: ReactNode }> = ({ children }) => {
  const { isAdmin } = useAuthStateValue();

  return isAdmin ? <>{children}</> : <Navigate to="/" />;
};

export default PProtectedRoute;
