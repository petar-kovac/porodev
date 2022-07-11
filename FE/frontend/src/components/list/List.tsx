import { FileZipOutlined } from '@ant-design/icons';
import { Avatar, List } from 'antd';
import { usePageContext } from 'context/PageContext';
import React from 'react';
import styled from 'styled-components';
import { formatDate } from 'util/helpers/date-formaters';

const PList: React.FC<{ data?: unknown[] | undefined; onSelectImage: any }> = ({
  data,
  onSelectImage,
}) => {
  const { setIsModalVisible } = usePageContext();
  return (
    <List
      dataSource={data}
      renderItem={(item: any) => (
        <StyledListItem
          aria-hidden="true"
          onClick={() => {
            setIsModalVisible(false);
            onSelectImage(item);
          }}
          key={item.fileName}
        >
          <List.Item.Meta
            // avatar={<Avatar src={item.avatar} />}
            title={item.fileName}
            description={item.uploadTime}
          />

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
