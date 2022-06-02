import { FC } from 'react';
import { BrowserRouter as Router } from 'react-router-dom';
import AuthProvider from './context/AuthContext';
import ApplicationProvider from './context/ApplicationContext';
import PRouter from './router/PRouter';

const App: FC = () => {
  return (
    <Router>
      <ApplicationProvider>
        <AuthProvider>
          <PRouter />
        </AuthProvider>
      </ApplicationProvider>
    </Router>
  );
};

export default App;
