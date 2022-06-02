import { FC, useState } from 'react';
import AuthProvider from './context/AuthContext';
import PRouter from './router/PRouter';
import ApplicationProvider from './context/ApplicationContext';

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
