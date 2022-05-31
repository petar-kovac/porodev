import React, { useState, FC } from 'react';
import { BrowserRouter as Router } from 'react-router-dom';
import Main from './Main';
// import Page from './layout/PLayout';
// import Login from './pages/login/Login';
const App: FC<any> = () => {
  // const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  // return <div>{isLoggedIn ? <Page /> : <Login />}</div>;
  return (
    <Router>
      <Main />
    </Router>
  );
};

export default App;
