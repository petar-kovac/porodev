import { Button, message } from 'antd';
import { FC, useEffect, useState } from 'react';
import { useAuthStateValue } from '../../context/AuthContext';

const Test: FC = () => {
  const { isAuthenticated, testMessage } = useAuthStateValue();

  return (
    <>
      <div>Test</div>
      <div>{testMessage}</div>
    </>
  );
};

export default Test;
