import { FC, useEffect } from 'react';
import styled from 'styled-components';
import PTable from '../../components/table/PTable';
import useAdminsColumns from './hooks/useAdminsColumns';
import useAdminsData from './hooks/useAdminsData';

const Admins: FC = () => {
  const { columns } = useAdminsColumns();
  const { isLoading, data, error, findData } = useAdminsData();

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
const StyledSpinnerWrapper = styled.div`
  display: flex;
  flex: 1;
  justify-content: center;
`;

export default Admins;
