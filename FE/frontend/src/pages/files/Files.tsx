import axios from 'axios';
import { FC, useEffect } from 'react';
import { findFiles } from '../../service/files/files';

const Files: FC = () => {
  useEffect(() => {
    const fetchFiles = async () => {
      try {
        const res = await axios.get(`${process.env.REACT_APP_MOCK_URL}/files`);
      } catch (err) {
        console.log(err);
      }
    };
    fetchFiles();
  }, []);

  return <div>Files</div>;
};

export default Files;
