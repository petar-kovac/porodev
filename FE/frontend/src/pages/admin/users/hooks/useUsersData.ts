import { useCallback, useEffect, useState } from 'react';
import axios from 'axios';
import { readAllUsers } from 'service/shared-spaces/shared-spaces';

const useUsersData = () => {
  const [isLoading, setisLoading] = useState<boolean>(false);
  const [data, setData] = useState<[] | undefined>(undefined);
  const [error, setError] = useState<string>('');

  useEffect(() => {
    const fetchFiles = async () => {
      setisLoading(true);
      try {
        const res = await readAllUsers();
        setData(res);
      } catch (err: any) {
        setError(err.message);
      } finally {
        setisLoading(false);
      }
    };
    fetchFiles();
  }, []);

  return {
    isLoading,
    data,
    error,
    setData,
    setError,
  };
};

export default useUsersData;
