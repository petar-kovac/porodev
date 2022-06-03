import { FC, useEffect } from 'react';
import { useAuthStateValue } from '../../context/AuthContext';

const Home: FC = () => {
  const { isAuthenticated, testMessage } = useAuthStateValue();

  return <div>Homeeee</div>;
};

export default Home;
