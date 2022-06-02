import axios from 'axios';
import { FC, useEffect, useState } from 'react';
import { useAuthStateValue } from '../../context/AuthContext';
import { loginApi } from '../../service/authorization/authorization';
import api from '../../service/base';

const Home: FC = () => {
  const { isAuthenticated, testMessage } = useAuthStateValue();

  return <div>Homeee</div>;
};

export default Home;
