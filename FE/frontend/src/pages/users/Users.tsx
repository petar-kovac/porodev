import { FC, useEffect } from 'react';
import styled from 'styled-components';
import PTable from '../../components/table/PTable';
import useUsersColumns from './hooks/useUsersColumns';
import useUsersData from './hooks/useUsersData';

const Users: FC = () => {
  const { columns } = useUsersColumns();
  const { isLoading, data, error, findData } = useUsersData();

  useEffect(() => {
    findData();
  }, []);

  return (
    <StyledPage>
      <PTable dataSource={data} columns={columns} />
    </StyledPage>
  );
};

const StyledPage = styled.div`
  padding: 20px;
`;

export default Users;
