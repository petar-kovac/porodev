import { FC, useState } from 'react';
import AuthProvider from './context/AuthContext';
// import Page from './layout/PLayout';
import PRouter from './router/PRouter';
import ApplicationProvider from './context/ApplicationContext';
// import Login from './pages/login/Login';
// import PRouter from './router/PRouter';

const Main: FC = () => {
  return (
    <ApplicationProvider>
      <AuthProvider>
        <PRouter />
      </AuthProvider>
    </ApplicationProvider>
  );
};

export default Main;
