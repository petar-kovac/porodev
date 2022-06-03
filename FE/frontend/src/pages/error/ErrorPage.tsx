import axios from 'axios';
import { FC, useEffect, useState } from 'react';
import { useAuthStateValue } from '../../context/AuthContext';
import { loginApi } from '../../service/authorization/authorization';
import api from '../../service/base';

const Error: FC = () => {
  const { isAuthenticated, testMessage } = useAuthStateValue();

  return <h1>404</h1>;
};

export default Error;
