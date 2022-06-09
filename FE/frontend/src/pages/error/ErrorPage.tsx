import axios from 'axios';
import { FC, useEffect, useState } from 'react';

import { loginApi } from '../../service/authorization/authorization';
import api from '../../service/base';

import { useAuthStateValue } from '../../context/AuthContext';

const Error: FC<{ message: string }> = ({ message }) => {
  const { isAuthenticated, testMessage } = useAuthStateValue();

  return (
    <>
      <h1>Error page</h1>
      <h1>{message}</h1>
    </>
  );
};

export default Error;
