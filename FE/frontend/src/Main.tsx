import { FC, useState } from 'react';
import AuthProvider from './context/AuthContext';
// import Page from './layout/PLayout';
import PRouter from './router/PRouter';
// import Login from './pages/login/Login';
// import PRouter from './router/PRouter';

const Main: FC = () => {
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(true);
  return (
    <AuthProvider>
      <PRouter />
    </AuthProvider>
  );
};

export default Main;
