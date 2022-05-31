import { createContext, FC, useContext, useState } from 'react';

type AuthContextProps = {
  isAuthenticated: boolean;
  testMessage: string;
};

export const AuthContext = createContext<AuthContextProps>({
  isAuthenticated: false,
  testMessage: '',
});

export const AuthConsumer = AuthContext.Consumer;

const AuthProvider: FC<any> = ({ children }) => {
  const isAuthenticated = false;
  const testMessage = 'cedo-cedo';
  const state: AuthContextProps = {
    isAuthenticated,
    testMessage,
  };
  return <AuthContext.Provider value={state}>{children}</AuthContext.Provider>;
};

export const useAuthStateValue: () => AuthContextProps = () =>
  useContext(AuthContext);

export default AuthProvider;
