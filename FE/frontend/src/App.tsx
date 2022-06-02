import { FC } from 'react';
import { BrowserRouter as Router } from 'react-router-dom';
import Main from './Main';

const App: FC = () => {
  return (
    <Router>
      <Main />
    </Router>
  );
};

export default App;
