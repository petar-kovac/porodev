import { FileZipOutlined } from '@ant-design/icons';
import { List } from 'antd';
import { usePageContext } from 'context/PageContext';
import { useSiderContext } from 'context/SiderContext';
import React, { Dispatch, SetStateAction } from 'react';
import styled from 'styled-components';

const PList: React.FC<{
  data?: unknown[] | undefined;
  setImageParameters?: Dispatch<SetStateAction<string[]>>;
}> = ({ data, setImageParameters = () => undefined }) => {
  const { setIsModalVisible } = usePageContext();

  return (
    <List
      dataSource={data}
      renderItem={(item: any) => (
        <StyledListItem
          aria-hidden="true"
          onClick={() => {
            setImageParameters([item.fileId]);
            setIsModalVisible(false);
          }}
          key={item.fileName}
        >
          <List.Item.Meta title={item.fileName} description={item.uploadTime} />
          <StyledSelect>
            <FileZipOutlined />
          </StyledSelect>
        </StyledListItem>
      )}
    />
  );
};

const StyledSelect = styled.div`
  cursor: pointer;
`;
const StyledListItem = styled(List.Item)`
  cursor: pointer;
  /* &:hover,
  &:active,
  &:focus {
    border: 2px solid red;
  } */
`;

const StyledDescription = styled.div`
  display: flex;
  justify-content: space-between;
`;

export default PList;
