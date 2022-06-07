import { FC, useEffect } from 'react';
import PUpload from '../../components/upload/PUpload';
import { useAuthStateValue } from '../../context/AuthContext';

const Home: FC = () => {
  const { isAuthenticated, testMessage } = useAuthStateValue();

  return (
    <div>
      <h1>CEDO</h1>
      <PUpload />
    </div>
  );
};

export default Home;
