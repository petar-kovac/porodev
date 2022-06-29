import { FC, useEffect } from 'react';

import PButton from 'components/buttons/PButton';
import PTable from 'components/table/PTable';
import { PAGES } from 'util/constants/constants';
import {
  StyledHeading,
  StyledHeadingWrapper,
  StyledPage,
} from './admins-styled';

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
      <StyledHeadingWrapper>
        <StyledHeading>{PAGES.admins}</StyledHeading>
        <PButton
          text="Add admin"
          color="#000"
          radius="12px"
          background="#fff"
        />
      </StyledHeadingWrapper>
      <PTable dataSource={data} columns={columns} />
    </StyledPage>
  );
};

export default Admins;
