import { useCallback, useState } from 'react';
import axios from 'axios';

const useUsersData = () => {
  const [isLoading, setisLoading] = useState<boolean>(false);
  const [data, setData] = useState<[] | undefined>(undefined);
  const [error, setError] = useState<string>('');

  const findData = useCallback(() => {
    (async function () {
      try {
        setisLoading(true);
        await axios
          .get(`${process.env.REACT_APP_MOCK_URL}/admins`)
          .then((res) => setData(res.data));
      } catch (err: any) {
        setError(err.message);
      } finally {
        setisLoading(false);
      }
    })();
  }, []);

  return {
    isLoading,
    data,
    error,
    findData,
    setData,
    setError,
  };
};

export default useUsersData;
