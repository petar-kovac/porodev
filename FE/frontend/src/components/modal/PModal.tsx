import { UserOutlined } from '@ant-design/icons';
import { Button, Modal, Input } from 'antd';
import { yupResolver } from '@hookform/resolvers/yup';

import { usePageContext } from 'context/PageContext';
import useGroupData from 'pages/user/groups/hooks/useGroupData';
import { Dispatch, FC, SetStateAction, ReactNode, useEffect } from 'react';
import { postPassword, postProfile } from 'service/files/files';
import { IRuntimeRsponse } from 'service/runtime/runtime.props';
import { Controller, useForm } from 'react-hook-form';

import {
  createSharedSpace,
  getAllSharedSpaces,
} from 'service/shared-spaces/shared-spaces';
import styled from 'styled-components';
import { passwordSchema } from '../../util/validation-schema/ValidationSchema';
// import { IFilesCard } from 'types/card-data';

import {
  StyledForm,
  StyledFormBox,
  StyledFormInput,
  StyledFormSpan,
  StyledHeader,
} from '../login/StyledForm';

interface IPModalProps {
  title?: string;
  content?: ReactNode;
  onOk?: any;
  data?: any;
  onCancel?: any;
  setModalData?: any;
  modalData?: any | null;
  passData?: any;
  setPassData?: any;
  inputField?: string;
}

const PModal: FC<IPModalProps> = ({
  title,
  content,
  modalData,
  data,
  setModalData = () => undefined,
  onOk,
  onCancel,
  inputField,
  passData,
  setPassData,
}) => {
  const {
    isModalVisible,
    setIsModalVisible,
    setSharedSpaceId,
    sharedSpaceId,
    isLoading,
    setIsLoading,
  } = usePageContext();
  const { setData } = useGroupData();

  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm<any>({
    resolver: yupResolver(passwordSchema),
  });

  const handleOk = async () => {
    if (inputField === 'sharedspace') {
      const space = await createSharedSpace(modalData);
      const res = await getAllSharedSpaces();
      setSharedSpaceId(!sharedSpaceId);
      setIsModalVisible(false);
    } else if (inputField === 'password') {
      setIsLoading(true);
      try {
        await postPassword(passData);
      } catch (error) {
        console.log(error);
        setIsLoading(false);
      }

      setIsLoading(false);
      setIsModalVisible(false);
    } else {
      setIsLoading(true);
      await postProfile(modalData);
      setIsLoading(false);
      setIsModalVisible(false);
    }
    setIsModalVisible(false);
  };

  const handleCancel = () => {
    setIsModalVisible(false);
  };

  //
  useEffect(() => {
    console.log(passData);
  }, [passData]);

  return (
    <>
      <StyledFilesModal
        title={title}
        visible={isModalVisible}
        okButtonProps={{
          loading: isLoading,
          form: 'passwordForm',
        }}
        onOk={inputField === 'password' ? handleSubmit(handleOk) : handleOk}
        onCancel={handleCancel}
      >
        <>
          {inputField === 'firstname' && (
            <Input
              onChange={(e) =>
                setModalData({
                  ...modalData,
                  name: e.target.value,
                })
              }
              value={modalData?.content as string}
            />
          )}
          {inputField === 'lastname' && (
            <Input
              onChange={(e) =>
                setModalData({ ...modalData, lastname: e.target.value })
              }
              value={modalData?.content as string}
            />
          )}
          {inputField === 'position' && (
            <Input
              onChange={(e) =>
                setModalData({ ...modalData, position: e.target.value })
              }
              value={modalData?.content as string}
            />
          )}

          {inputField === 'password' && (
            <>
              <form id="passwordForm">
                <Controller
                  name="oldPassword"
                  control={control}
                  defaultValue=""
                  render={({ field: { ref, onChange, ...field } }) => (
                    <StyledFormBox>
                      <span>Old password:</span>
                      <Input.Password
                        {...field}
                        type="password"
                        onChange={({ target: { value } }) => {
                          setPassData({ ...passData, oldPassword: value });
                          onChange(value);
                          console.log(passData);
                        }}
                        placeholder="Enter your old password..."
                      />
                      <StyledFormSpan>
                        {errors?.oldPassword?.message}
                      </StyledFormSpan>
                    </StyledFormBox>
                  )}
                />
                <Controller
                  name="newPassword"
                  control={control}
                  defaultValue=""
                  render={({ field }) => (
                    <StyledFormBox>
                      <span>Password:</span>
                      <Input.Password
                        {...field}
                        type="password"
                        autoComplete="new-password"
                        placeholder="Enter your new password..."
                      />
                      <StyledFormSpan>
                        {errors?.newPassword?.message}
                      </StyledFormSpan>
                    </StyledFormBox>
                  )}
                />

                <Controller
                  name="confirmPassword"
                  control={control}
                  defaultValue=""
                  render={({ field: { ref, onChange, ...field } }) => (
                    <StyledFormBox>
                      <span>Confirm password:</span>
                      <Input.Password
                        {...field}
                        type="password"
                        onChange={({ target: { value } }) => {
                          setPassData({ ...passData, newPassword: value });
                          onChange(value);
                          console.log(passData);
                        }}
                        placeholder="Confirm your new password..."
                      />
                      <StyledFormSpan>
                        {errors?.confirmPassword?.message}
                      </StyledFormSpan>
                    </StyledFormBox>
                  )}
                />
              </form>
            </>
          )}

          {inputField === 'sharedspace' && (
            <Input
              style={{ borderRadius: 12 }}
              onChange={(e) =>
                setModalData({ ...modalData, name: e.target.value })
              }
              value={modalData?.content as string}
            />
          )}
        </>
      </StyledFilesModal>
    </>
  );
};

const StyledFilesModal = styled(Modal).attrs({
  'data-testid': 'modal',
})`
  .ant-modal-header {
    border-radius: 1.2rem;
    background-color: rgba(220, 220, 220, 0.1);
    border: 1px solid #fff;
    border-bottom: 1px solid #eee;
  }

  .ant-modal-title {
    color: #555;
    letter-spacing: 0.5px;
    font-size: 1.8rem;
  }

  .ant-modal-content {
    border-radius: 1.2rem;
    box-shadow: 1px 3px 4px rgba(255, 255, 255, 0.4);
  }

  .ant-modal-footer {
    padding: 1.4rem 2.2rem;
    border-radius: 1.6rem;

    button {
      border-radius: 0.8rem;
    }

    .footer-content {
      display: flex;
      justify-content: space-between;
      align-items: center;
    }
  }
`;

export default PModal;
