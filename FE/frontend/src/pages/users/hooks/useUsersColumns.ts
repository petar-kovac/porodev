import { SearchOutlined } from '@ant-design/icons';
import { useMemo, useState } from 'react';

const useUsersColumns = () => {
  const [filterData, setFilterData] = useState([]);
  const [currentColumn, setCurrentColumn] = useState('all');
  const columns = useMemo(
    () => [
      {
        title: 'Name',
        dataIndex: 'name',
        key: 'name',
      },
      {
        title: 'Age',
        dataIndex: 'age',
        key: 'age',
      },
      {
        title: 'Address',
        dataIndex: 'address',
        key: 'address',
      },
    ],
    [],
  );

  return { columns };
};

export default useUsersColumns;
