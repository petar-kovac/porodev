import { FC } from 'react';
import { BrowserRouter as Router } from 'react-router-dom';
import { ThemeProvider } from 'styled-components';
import theme from 'theme/theme';
import AuthProvider from './context/AuthContext';
import ApplicationProvider from './context/ApplicationContext';
import PRouter from './router/PRouter';

const App: FC = () => {
  return (
    <Router>
      <ThemeProvider theme={theme}>
        <ApplicationProvider>
          <AuthProvider>
            <PRouter />
          </AuthProvider>
        </ApplicationProvider>
      </ThemeProvider>
    </Router>
  );
};

export default App;
